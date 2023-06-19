using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sp23Team33FinalProject.DAL;
using sp23Team33FinalProject.Models;

namespace sp23Team33FinalProject.Controllers
{
    [Authorize]
    public class InterviewsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public InterviewsController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Interviews
        public async Task<IActionResult> Index()
        {
            var query = from i in _context.Interviews select i;
            if (User.IsInRole("Student") == true)
            {
                query = query.Where(i => i.Student.UserName == User.Identity.Name);
            }

            if (User.IsInRole("Recruiter") == true)
            {
                // find recruiter
                AppUser recruiter = await _context.Users.Include(u => u.Company)
                    .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                // restrict query to only application for positions in rec's company
                query = query.Where(i => i.Position.Company == recruiter.Company);

            }

            List<Interview> SelectedIntervierws = await query.Include(i => i.Host)
                                                                .Include(a => a.Position)
                                                                .ThenInclude(p => p.Company)
                                                                .Include(a => a.Application)
                                                                .Include(i => i.Student)
                                                                .ToListAsync();

            return View(SelectedIntervierws.OrderBy(a => a.InterviewDateTime));
        }

        // GET: Interviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Interviews == null)
            {
                return NotFound();
            }

            var interview = await _context.Interviews
                .Include(i => i.Student)
                .Include(i => i.Application)
                .ThenInclude(i => i.Position).ThenInclude(p => p.Company)
                .Include(i => i.Host)
                .FirstOrDefaultAsync(m => m.InterviewID == id);

            if (interview == null)
            {
                return NotFound();
            }

            return View(interview);
        }

        // GET: Interviews/Create
        public IActionResult Create()
        {
            ViewData["AppForeignKey"] = new SelectList(_context.Applications, "ApplicationID", "ApplicationID");
            return View();
        }

        // POST: Interviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InterviewID,InterviewDateTime,Room,AppForeignKey")] Interview interview)
        {
            if (ModelState.IsValid)
            {
                _context.Add(interview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppForeignKey"] = new SelectList(_context.Applications, "ApplicationID", "ApplicationID", interview.AppForeignKey);
            return View(interview);
        }

        // edit interview host as  drop down list to be anyone from the company

        // GET: Interviews/Edit/5
        [Authorize(Roles ="Recruiter, CSO")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Interviews == null)
            {
                return NotFound();
            }

            var interview = await _context.Interviews.Include(i => i.Host).FirstOrDefaultAsync(i => i.InterviewID == id);

            if (interview == null)
            {
                return NotFound();
            }
            ViewBag.AllHosts = await GetAllHostsAsync(interview.Host.Id);
            //ViewData["AppForeignKey"] = new SelectList(_context.Applications, "ApplicationID", "ApplicationID", interview.AppForeignKey);
            return View(interview);
        }

        // POST: Interviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Recruiter, CSO")]
        public async Task<IActionResult> Edit(int id, Interview interview)
        {
            if (id != interview.InterviewID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(interview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterviewExists(interview.InterviewID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["AppForeignKey"] = new SelectList(_context.Applications, "ApplicationID", "ApplicationID", interview.AppForeignKey);
            ViewBag.AllHosts = await GetAllHostsAsync(interview.Host.Id);
            return View(interview);
        }

        // GET: Interviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Interviews == null)
            {
                return NotFound();
            }

            var interview = await _context.Interviews
                .Include(i => i.Application)
                .ThenInclude(i => i.Position)
                .Include(i => i.Host)
                .Include(i => i.Student)


                .FirstOrDefaultAsync(m => m.InterviewID == id);
            if (interview == null)
            {
                return NotFound();
            }

            ISession session = HttpContext.Session;
            string dateString = session.GetString("AppDate");
            DateTime AppDate = DateTime.Parse(dateString);

            if (AppDate.AddHours(24) >= interview.InterviewDateTime)
            {
                return View("Error", new String[] { "Cannot cancel interview within 24 hours of start time!" });
            }


            return View(interview);
        }

        // POST: Interviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Interviews == null)
            {
                return Problem("Entity set 'AppDbContext.Interviews'  is null.");
            }

            var interview = await _context.Interviews.Include(i => i.Student).Include(i => i.Application).FirstOrDefaultAsync(i => i.InterviewID == id);

            if(interview.Student != null)
            {
                AppUser student = await _context.Users.Include(s => s.StudentInterviews).FirstOrDefaultAsync(u => u.UserName == interview.Student.UserName);
                if (student != null)
                {
                    student.StudentInterviews.Remove(interview);
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
            }
            
            if(interview.Application != null)
            {
                Application application = await _context.Applications.Include(a => a.Interview).FirstOrDefaultAsync(a => a.ApplicationID == interview.Application.ApplicationID);
                if (application != null)
                {
                    application.Interview = null;
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
            }

            interview.Student = null;
            interview.Application = null;

            _context.Update(interview);
            await _context.SaveChangesAsync();
            
            if(User.IsInRole("Recruiter") || User.IsInRole("CSO"))
            {
                if (interview != null)
                {
                    _context.Interviews.Remove(interview);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Recruiter, CSO")]
        public async Task<IActionResult> StudentScheduler()
        {
            List<Interview> SelectedInterviews;

            if (User.IsInRole("Admin"))
            {
                SelectedInterviews = await _context.Interviews
                                 .Include(o => o.Application)
                                 .ThenInclude(o => o.Position)
                                 .Include(o => o.Host)
                                 .ToListAsync();
            }
            if (User.IsInRole("Student")) //user is a customer
            {
                AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                SelectedInterviews = await _context.Interviews
                                 .Include(o => o.Application)
                                 .ThenInclude(o => o.Position)
                                 .Include(o => o.Host)
                                 .Where(o => o.Student.UserName == null)
                                 .ToListAsync();
            }

            else
            {
                return View();
            }

            return View(SelectedInterviews.OrderBy(o => o.InterviewID));
        }


        [Authorize(Roles = "Recruiter, CSO")]
        // GET: Interviews/SchedMaker
        public IActionResult SchedMaker(int? id)
        {

            ViewBag.AllPositions = GetAllPositionsAsync();
            //ViewBag.AllHosts = GetAllHostsAsync();
            ViewBag.AllHosts = GetAllHostsSelectListAsync();
            return View();

        }

        // POST: Interviews/SchedMaker
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Recruiter, CSO")]
        public async Task<IActionResult> SchedMaker(int id, [Bind("InterviewID,InterviewDateTime,Room,Position,Host")] Interview interview)
        {

            if (User.IsInRole("Recruiter") || User.IsInRole("CSO"))
            {
                //var company = from b in _context.Companies select b;
                ViewBag.AllHosts = await GetAllHostsAsync();

                var appDbContext = _context.Interviews.Include(i => i.Application).Include(i => i.Position)
                                                      .Include(i => i.Host).Include(i => i.InterviewID).
                                                      Include(i => i.InterviewDateTime);

                //var appDbContext = _context.Interviews.Include(i => i.Application);
                return View(await appDbContext.ToListAsync());
            }

            else
            {
                return View();
            }

        }

        // TODO add authroize tag
        // GET: Applications/Create
        [Authorize(Roles = "Recruiter, CSO")]
        public async Task<IActionResult> Invite(int? positionID)
        {
            if (positionID == null) { return View("Error", new String[] { "Please provide a position to invite student for!" }); }
            Position position = await _context.Positions.FirstOrDefaultAsync(p => p.PositionID == positionID);
            if(position == null)
            {
                return View("Error", new String[] { "Position does not exist!" });
            }
            ViewBag.AllStudents = GetAllStudentsMultiSelectList();

            InviteStudentViewModel vm = new InviteStudentViewModel();
            vm.PositionID = position.PositionID;

            return View(vm);
        }

        //POST: Applications/Invite
        //To protect from overposting attacks, enable the specific properties you want to bind to.
        //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        //TODO add authroize tag

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Recruiter, CSO")]
        public async Task<IActionResult> Invite(InviteStudentViewModel vm)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.AllStudents = GetAllStudentsMultiSelectList();
                return View(vm);
            }

            List<AppUser> students = await _context.Users.Where(s => vm.Students.Contains(s.Id)).ToListAsync();
            Position position = await _context.Positions.FirstOrDefaultAsync(p => p.PositionID == vm.PositionID);

            foreach(AppUser student in students)
            {
                bool hasApplied = await _context.Applications.AnyAsync(a => a.Position == position && a.Student == student);

                if (hasApplied)
                {
                    continue;
                }

                // Create a new application for the student
                Application newApplication = new Application
                {
                    Position = position,
                    Student = student,
                    AppStatus = Status.Accepted
                };
                _context.Applications.Add(newApplication);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Positions");
        }


        private MultiSelectList GetAllStudentsMultiSelectList()
        {
            //Get the list of months from the database
            List<AppUser> studentList = _context.Users.ToList();
            studentList = studentList.Where(s => s.GraduationYear != null).ToList();

            MultiSelectList studentSelectList = new MultiSelectList(studentList.OrderBy(m => m.LastName), "Id", "Email");

            //return the MultiSelectList
            return studentSelectList;
        }

        private bool InterviewExists(int id)
        {
            return _context.Interviews.Any(e => e.InterviewID == id);
        }

        private async Task<MultiSelectList> GetAllPositionsAsync()
        {
            //Get the list of majors from the database
            List<Position> positionList = _context.Positions.ToList();
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            //convert the list to a SelectList by calling SelectList constructor
            SelectList positionSelectList = new SelectList(positionList.OrderBy(m => m.PositionID).Where(m => m.Company == appUser.Company), "PositionID", "PositionName");

            //return the SelectList
            return positionSelectList;
        }

        private async Task<SelectList> GetAllHostsAsync()
        {
            AppUser appUser = await _context.Users.Include(u => u.Company).ThenInclude(u => u.Recruiters).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            List<AppUser> allHosts = appUser.Company.Recruiters.ToList();

            SelectList slAllHosts = new SelectList(allHosts, "Id", "FirstName");

            return slAllHosts;
        }

        private async Task<SelectList> GetAllHostsAsync(String id)
        {
            AppUser appUser = await _context.Users.Include(u => u.Company).ThenInclude(u => u.Recruiters).FirstOrDefaultAsync(u => u.Id == id);
            List<AppUser> allHosts = appUser.Company.Recruiters.ToList();

            SelectList slAllHosts = new SelectList(allHosts, nameof(appUser.Id), nameof(appUser.Email));

            return slAllHosts;
        }
        private async Task<SelectList> GetAllHostsSelectListAsync()
        {
            AppUser appUser = await _context.Users.Include(u => u.Company).ThenInclude(u => u.Recruiters).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            List<AppUser> allHosts = appUser.Company.Recruiters.ToList();

            SelectList hostSelectList = new SelectList(allHosts.OrderBy(m => m.Id), "Id", "FirstName");

            return hostSelectList;
        }

        private SelectList GetAllStudentsSelectList()
        {

            var query2 = from s in _context.Users select s;
            // filter to only include students
            query2 = query2.Where(s => s.GraduationYear != null);

            List<AppUser> SelectedStudents = query2.ToList();

            ViewBag.AllStudents = SelectedStudents;

            SelectList studentSelectList = new SelectList(SelectedStudents.OrderBy(m => m.Id), "Id", "Email");

            return studentSelectList;
        }

        [Authorize(Roles = "Recruiter, CSO")]
        // could add functionality to add another user instead
        public async Task<IActionResult> ReserveRoom(int? positionID)
        {
            if(User.IsInRole("CSO"))
            {
                ReserveRoomViewModel vm = new ReserveRoomViewModel();
                vm.HostUserName = User.Identity.Name;
                vm.StartAvail = DateTime.Now.Date;
                vm.EndAvail = DateTime.Now.Date;
                return View(vm);
            }
            if(positionID == null)
            {
                return View("Error", new String[] { "Please specify a position to reserve rooms!" });
            }

            Position position = await _context.Positions.FirstOrDefaultAsync(p => p.PositionID == positionID);

            if (position == null)
            {
                return View("Error", new String[] { "This position does not exist!" });
            }

            ReserveRoomViewModel rrvm = new ReserveRoomViewModel();
            rrvm.HostUserName = User.Identity.Name;
            rrvm.PositionID = (int)positionID;
            rrvm.StartAvail = DateTime.Now.Date;
            rrvm.EndAvail = DateTime.Now.Date;

            return View(rrvm);
        }

        [Authorize(Roles = "Recruiter, CSO")]
        public async Task<IActionResult> ConfirmReserveRoom(ReserveRoomViewModel rrvm)
        {
            if (ModelState.IsValid == false)
            {
                return View("ReserveRoom", rrvm);
            }

            if(User.IsInRole("Recruiter"))
            {
                Position position = await _context.Positions.FirstOrDefaultAsync(p => p.PositionID == rrvm.PositionID);
                if (rrvm.StartAvail < position.Deadline.AddHours(48))
                {
                    return View("Error", new String[] { "Can only reserve rooms for positions 48 hours after deadline!" });
                }
            }
            var query = _context.Interviews;
            //Position position = await _context.Positions.FirstOrDefaultAsync(p => p.PositionID == rrvm.PositionID);

            ISession session = HttpContext.Session;
            // Retrieve the date from the session and convert it back to a DateTime object
            string dateString = session.GetString("AppDate");
            DateTime AppDate = DateTime.Parse(dateString);

            if (rrvm.StartAvail > rrvm.EndAvail)
            {
                return View("Error", new String[] { "Start time must be after end time!" });
            }
            if (query.Any(i => i.Room == rrvm.Room && i.InterviewDateTime >= rrvm.StartAvail && i.InterviewDateTime <= rrvm.EndAvail))
            {
                return View("Error", new String[] { "There is a conflicting reservation for this room!" });
            }
            if (rrvm.StartAvail.DayOfWeek == DayOfWeek.Saturday || rrvm.StartAvail.DayOfWeek == DayOfWeek.Sunday)
            {
                return View("Error", new String[] { "Start time must fall on weekday!" });
            }
            if (rrvm.EndAvail.DayOfWeek == DayOfWeek.Saturday || rrvm.EndAvail.DayOfWeek == DayOfWeek.Sunday)
            {
                return View("Error", new String[] { "End time must fall on weekday!" });
            }
            if (rrvm.StartAvail.Hour < 8)
            {
                int flushBegTime = 8 - rrvm.StartAvail.Hour;
                rrvm.StartAvail = rrvm.StartAvail.AddHours(flushBegTime);
            }
            if (rrvm.EndAvail.Hour > 17)
            {
                int flushEndTime = 17 - rrvm.EndAvail.Hour;
                rrvm.EndAvail = rrvm.EndAvail.AddHours(flushEndTime);
            }

            AppUser host = await _context.Users.Include(u => u.HostRoomBookings).FirstOrDefaultAsync(u => u.UserName == rrvm.HostUserName);

            TimeSpan timeDifference = rrvm.EndAvail - rrvm.StartAvail;
            int totalHours = (int)timeDifference.TotalHours;
            for (int i = 0; i < totalHours; i++)
            {
                Interview interviewI = new Interview();
                if(rrvm.StartAvail.Hour == 12)
                {
                    rrvm.StartAvail = rrvm.StartAvail.AddHours(1);
                }
                if (rrvm.StartAvail.Hour == 17)
                {
                    if (rrvm.StartAvail.DayOfWeek == DayOfWeek.Friday)
                    {
                        rrvm.StartAvail = rrvm.StartAvail.AddDays(2);
                    }
                    rrvm.StartAvail = rrvm.StartAvail.AddHours(15);
                }
                if(rrvm.StartAvail >= rrvm.EndAvail)
                {
                    break;
                }
                interviewI.InterviewDateTime = rrvm.StartAvail;
                interviewI.Host = host;
                if(User.IsInRole("Recruiter"))
                {
                    Position position = await _context.Positions.FirstOrDefaultAsync(p => p.PositionID == rrvm.PositionID);
                    interviewI.Position = position;
                }
                interviewI.Room = rrvm.Room;
                host.HostRoomBookings.Add(interviewI);

                //Add product to database
                _context.Add(interviewI);
                _context.Update(host);
                await _context.SaveChangesAsync();

                rrvm.StartAvail = rrvm.StartAvail.AddHours(1);
            }
           
            return View("Confirm");
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> SelectInterview(int? positionID)
        {
            if (positionID == null)
            {
                return View("Error", new String[] { "Please specify a position to reserve rooms!" });
            }

            Position position = await _context.Positions.Include(p => p.Company).FirstOrDefaultAsync(p => p.PositionID == positionID);

            if (position == null)
            {
                return View("Error", new String[] { "This position does not exist!" });
            }
            AppUser student = await _context.Users.Include(u => u.StudentInterviews).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            if (student.StudentInterviews.Any(i => i.Position == position))
            {
                return View("Error", new String[] { "Cannot reserve multiple interviews for same position!!" });
            }

            List<Interview> interviewList = _context.Interviews.ToList();

            interviewList = interviewList.Where(i => i.Position == position).ToList();
            interviewList = interviewList.Where(i => i.Student == null && i.Application == null).OrderBy(i => i.InterviewDateTime).ToList();

            //ViewBag.AllInterviews = GetAllInterviewsSelectList(position);
            InterviewSelectViewModel isvm = new InterviewSelectViewModel();

            //isvm.UserName = User.Identity.Name;
            isvm.PositionID = position.PositionID;
            isvm.Interviews = interviewList;
            isvm.PositionName = position.PositionTitle;
            isvm.CompanyName = position.Company.CompanyName;

            return View(isvm);
        }

        private SelectList GetAllInterviewsSelectList(Position position)
        {
            //Get the list of months from the database
            List<Interview> interviewList = _context.Interviews.ToList();

            interviewList = interviewList.Where(i => i.Position == position).ToList();
            interviewList = interviewList.Where(i => i.Student == null && i.Application == null).ToList();

            //convert the list to a SelectList by calling SelectList constructor
            //MonthID and MonthName are the names of the properties on the Month class
            //MonthID is the primary key
            SelectList interviewSelectList = new SelectList(interviewList.OrderBy(i => i.InterviewID), "InterviewID", "InterviewDateTime");

            //return the electList
            return interviewSelectList;
        }

        [Authorize(Roles = "Student")]
        [HttpPost]
        public async Task<IActionResult> ConfirmInterviewTime(InterviewSelectViewModel isvm)
        {
            if (ModelState.IsValid == false)
            {
                return View("SelectInterview", isvm);
            }

            //add 12 if numbergreater than = 1
            if(isvm.SelectedHour <= 4 && isvm.SelectedHour >= 1)
            {
                isvm.SelectedHour = isvm.SelectedHour + 12;
            }
            DateTime datetime = (DateTime)isvm.SelectedDate;
            datetime = datetime.AddHours((double)isvm.SelectedHour);

            Interview interview = await _context.Interviews.Include(i => i.Student).FirstOrDefaultAsync(i => i.Position.PositionID == isvm.PositionID && i.InterviewDateTime == datetime );

            if (interview == null) { return View("Error", new String[] { "Please enter a valid interview!" }); }

            AppUser student = await _context.Users.Include(u => u.Applications).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            Application application = await _context.Applications.Include(a => a.Student).FirstOrDefaultAsync(a => a.Position.PositionID == isvm.PositionID);


            interview.Student = student;
            interview.Application = application;
            application.Interview = interview;
            student.StudentInterviews.Add(interview);

            _context.Update(interview);
            _context.Update(student);
            _context.Update(application);
            _context.SaveChanges();

            return View("ConfirmInterview");
        }

        [Authorize(Roles ="CSO, Recruiter")]
        public async Task<IActionResult> EditHost(int? interviewID)
        {
            if (interviewID == null)
            {
                return View("Error", new String[] { "Please specify a interview to edit host!" });
            }

            Interview interview = await _context.Interviews.Include(i => i.Position).ThenInclude(p => p.Company)
                .FirstOrDefaultAsync(i => i.InterviewID == interviewID);

            if (interview == null)
            {
                return View("Error", new String[] { "This interview does not exist!" });
            }

            List<AppUser> hostsList = _context.Users.ToList();

            hostsList = hostsList.Where(h => h.Company == interview.Position.Company).ToList();

            //ViewBag.AllInterviews = GetAllInterviewsSelectList(position);
            EditHostViewModel vm = new EditHostViewModel();

            vm.CompanyID = interview.Position.Company.CompanyID;
            vm.InterviewID = interview.InterviewID;
            vm.Hosts = hostsList;
            vm.CurrentHost = interview.Host.Email;
            //vm.InterviewDateTime = interview.InterviewDateTime;
            //vm.Room = interview.Room;
            
            return View(vm);
        }

        [Authorize(Roles = "CSO, Recruiter")]
        [HttpPost]
        public async Task<IActionResult> EditHost(EditHostViewModel vm)
        {
            if (ModelState.IsValid == false)
            {
                return View("EditHost", vm);
            }

            AppUser newHost = await _context.Users.Include(u => u.Company).FirstOrDefaultAsync(u => u.Email == vm.newHostEmail);

            if (newHost == null)
            {
                return View("Error", new String[] { "Please enter a valid new host!" });
            }

            if(newHost.Company.CompanyID != vm.CompanyID)
            {
                return View("Error", new String[] { "New Host must be from same company!" });
            }

            Interview interview = await _context.Interviews.Include(i => i.Host)
                .FirstOrDefaultAsync(i => i.InterviewID == vm.InterviewID);

            interview.Host = newHost;
            newHost.HostRoomBookings.Add(interview);

            _context.Update(interview);
            _context.Update(newHost);
            _context.SaveChanges();

            return RedirectToAction("Index", "Interviews");
        }

        [Authorize(Roles = "Recruiter, CSO")]
        public IActionResult RoomVisual()
        {
            List<Interview> interviews = _context.Interviews.ToList();
            return View(interviews);
        }
    }
}
