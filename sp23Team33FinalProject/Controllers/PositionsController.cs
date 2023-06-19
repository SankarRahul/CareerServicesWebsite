using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sp23Team33FinalProject.DAL;
using sp23Team33FinalProject.Models;
using sp23Team33FinalProject.Utilities;

namespace sp23Team33FinalProject.Controllers
{
    [Authorize]
    public class PositionsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;


        public PositionsController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Positions
        public async Task<IActionResult> Index()
        {
            var query = from p in _context.Positions select p;
            ViewBag.AllPositions = _context.Positions.Count();

            if (User.IsInRole("Student") == true)
            {
                ISession session = HttpContext.Session;
                string dateString = session.GetString("AppDate");
                DateTime AppDate = DateTime.Parse(dateString);

                query = query.Where(p => p.Deadline >= AppDate);
                ViewBag.AllPositions = query.Count();
            }

            // TODO restrict recuiters from changing other company details
            if (User.IsInRole("Recruiter") == true)
            {
                AppUser recruiter = await _context.Users.Include(u => u.Company)
                                                            .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

                query = query.Where(p => p.Company == recruiter.Company);
                ViewBag.AllPositions = query.Count();
            }

            // selects all positions in query
            List<Position> SelectedPositions = query.Include(p => p.Company).Include(p => p.Majors).ToList();

            ViewBag.SelectedPositions = SelectedPositions.Count();

            return View(SelectedPositions.OrderBy(p => p.Deadline));
        }

        // GET: Positions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Positions == null)
            {
                return View("Error", new String[] { "Please specify a product to view!" });
            }

            Position position = await _context.Positions
            .Include(p => p.Company)
            .Include(p => p.Majors)
            .FirstOrDefaultAsync(m => m.PositionID == id);

            if (position == null)
            {
                return View("Error", new String[] { "This product was not found in the database!" });
            }

            return View(position);
        }

        // GET: Positions/Create
        [Authorize(Roles = "Recruiter, CSO")]
        public async Task<IActionResult> Create()
        {
            ViewBag.AllCompanies = GetAllCompaniesSelectList();
            if (User.IsInRole("Recruiter"))
            {

                AppUser recruiter = await _context.Users.Include(u => u.Company).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                Company recCompany = await _context.Companies.Include(c => c.Positions).FirstOrDefaultAsync(c => c.Recruiters.Contains(recruiter));

                List<Company> companyList = new List<Company>();

                companyList.Add(recCompany);

                SelectList companySelectList = new SelectList(companyList.OrderBy(c => c.CompanyID), "CompanyID", "CompanyName");

                ViewBag.AllCompanies = companySelectList;

            }
            ViewBag.AllMajors = GetAllMajorsMultiSelectList();
            return View();
        }

        // POST: Positions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Recruiter, CSO")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PositionID,PositionTitle,PositionDescription,PositionType,Location,Deadline")] Position position, int SelectedCompany, int[] SelectedMajors)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.AllCompanies = GetAllCompaniesSelectList();
                ViewBag.AllMajors = GetAllMajorsMultiSelectList();
                return View(position);
            }

            position.Company = _context.Companies.Find(SelectedCompany);
            //Add product to database
            _context.Add(position);
            await _context.SaveChangesAsync();

            //add suppliers
            foreach (int majorID in SelectedMajors)
            {
                Major dbMajor = _context.Majors.Find(majorID);
                position.Majors.Add(dbMajor);
                await _context.SaveChangesAsync();
            }

            //go back to Index page
            return RedirectToAction(nameof(Index));
        }

        // GET: Positions/Edit/5
        [Authorize(Roles = "Recruiter, CSO")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("Error", new String[] { "Please specify a position to edit!" });
            }

            Position position = await _context.Positions.Include(p => p.Company).Include(p => p.Majors)
                                                     .FirstOrDefaultAsync(p => p.PositionID == id);


            if (position == null)
            {
                return View("Error", new String[] { "This position does not exist in the database!" });
            }

            if (User.IsInRole("Recruiter"))
            {
                AppUser recruiter = await _context.Users.Include(u => u.Company).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                Company recCompany = await _context.Companies.Include(c => c.Positions).FirstOrDefaultAsync(c => c.Recruiters.Contains(recruiter));
                if (position.Company != recCompany)
                {
                    return View("Error", new String[] { "You are not authorized to edit this position!" });
                }
                ISession session = HttpContext.Session;
                string dateString = session.GetString("AppDate");
                DateTime AppDate = DateTime.Parse(dateString);
                if (position.Deadline > AppDate)
                {
                    return View("Error", new String[] { "You are not authorized to edit positions after deadline!" });
                }
            }

            ViewBag.AllCompanies = GetAllCompaniesSelectList(position);
            ViewBag.AllMajors = GetAllMajorsMultiSelectList(position);
            return View(position);
        }


        // POST: Positions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Recruiter, CSO")]
        public async Task<IActionResult> Edit(int id, Position position, int SelectedCompany, int[] SelectedMajors)
        {
            if (id != position.PositionID)
            {
                return View("Error", new String[] { "There was an error.  Please try again." });
            }

            Position dbPosition = _context.Positions.Include(p => p.Company)
                                                    .Include(p => p.Majors)
                                                    .FirstOrDefault(p => p.PositionID == position.PositionID);

            if (dbPosition == null)
            {
                return View("Error", new String[] { "This position was not found! " });
            }

            if (ModelState.IsValid == false)
            {
                //re-populate the view bag
                ViewBag.AllCompany = GetAllCompaniesSelectList(dbPosition);
                ViewBag.AllMajors = GetAllMajorsMultiSelectList(dbPosition);


                //go back to the view
                return View(position);
            }

            try
            {
                //create the list of products to delete
                List<Major> MajorsToRemove = new List<Major>();

                //find the ones that aren't on the new list
                foreach (Major major in dbPosition.Majors)
                {
                    if (SelectedMajors.Contains(major.MajorId) == false) //existing major is not in new list
                    {
                        MajorsToRemove.Add(major);
                    }
                }

                //remove the items in the list
                foreach (Major major in MajorsToRemove)
                {
                    dbPosition.Majors.Remove(major);
                    await _context.SaveChangesAsync();
                }

                //now add all the new suppliers
                foreach (int majorID in SelectedMajors)
                {
                    if (dbPosition.Majors.Any(m => m.MajorId == majorID) == false)
                    {
                        //find the supplier in the database
                        Major dbMajor = _context.Majors.Find(majorID);

                        //add to list
                        dbPosition.Majors.Add(dbMajor);

                        //save changes
                        await _context.SaveChangesAsync();
                    }
                }

                //change remaining properties
                Company dbCompany = await _context.Companies
                    .Include(c => c.Positions)
                    .FirstOrDefaultAsync(c => c.CompanyID == SelectedCompany);

                dbPosition.PositionTitle = position.PositionTitle;
                dbPosition.PositionDescription = position.PositionDescription;
                dbPosition.PositionType = position.PositionType;
                dbPosition.Location = position.Location;
                dbPosition.Deadline = position.Deadline;
                dbPosition.Company = dbCompany;

                _context.Update(dbPosition);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return View("Error", new String[] { "There was a problem editing this product", ex.Message });
            }

            //This is the happy path - go to the index page
            return RedirectToAction(nameof(Index));
        }



        // GET: Positions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Positions == null)
            {
                return NotFound();
            }

            var position = await _context.Positions
            .FirstOrDefaultAsync(m => m.PositionID == id);
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        // POST: Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Positions == null)
            {
                return Problem("Entity set 'AppDbContext.Positions'  is null.");
            }
            var position = await _context.Positions.FindAsync(id);
            if (position != null)
            {
                _context.Positions.Remove(position);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PositionExists(int id)
        {
            return (_context.Positions?.Any(e => e.PositionID == id)).GetValueOrDefault();
        }

        public IActionResult PositionSearch()
        {
            ViewBag.AllCompanies = GetAllCompaniesMultiSelectList();
            //ViewBag.AllCompanies = GetAllCompaniesSelectList();
            ViewBag.AllMajors = GetAllMajorsMultiSelectList();
            //ViewBag.AllMajors = GetAllMajorsSelectList();
            return View();
        }


        private SelectList GetAllCompaniesSelectList()
        {
            //Get the list of months from the database
            List<Company> companyList = _context.Companies.ToList();

            //add a dummy entry so the user can select all months
            //Company SelectNone = new Company() { CompanyID = 0, CompanyName = "All Companies" };
            //companyList.Add(SelectNone);

            //convert the list to a SelectList by calling SelectList constructor
            SelectList companySelectList = new SelectList(companyList.OrderBy(c => c.CompanyID), "CompanyID", "CompanyName");

            //return the electList
            return companySelectList;
        }

        private SelectList GetAllCompaniesSelectList(Position position)
        {
            //Get the list of months from the database
            List<Company> companyList = _context.Companies.ToList();

            Int32 associatedCompanyID = position.Company.CompanyID;

            //convert the list to a SelectList by calling SelectList constructor
            SelectList companySelectList = new SelectList(companyList.OrderBy(c => c.CompanyID), "CompanyID", "CompanyName", associatedCompanyID);

            //return the electList
            return companySelectList;
        }

        private SelectList GetAllMajorsSelectList()
        {
            //Get the list of months from the database
            List<Major> majorList = _context.Majors.ToList();

            //add a dummy entry so the user can select all months
            Major SelectNone = new Major() { MajorId = 0, MajorName = "All Companies" };
            majorList.Add(SelectNone);

            //convert the list to a SelectList by calling SelectList constructor
            SelectList majorSelectList = new SelectList(majorList.OrderBy(m => m.MajorId), "MajorId", "MajorName");

            //return the electList
            return majorSelectList;
        }

        private MultiSelectList GetAllCompaniesMultiSelectList()
        {
            //Get the list of companies from the database
            List<Company> companyList = _context.Companies.ToList();

            //add a dummy entry so the user can select all companies
            Company SelectNone = new Company() { CompanyID = 0, CompanyName = "All Companies" };
            companyList.Add(SelectNone);

            //convert the list to a SelectList by calling SelectList constructor
            MultiSelectList companySelectList = new MultiSelectList(companyList.OrderBy(c => c.CompanyID), "CompanyID", "CompanyName");

            //return the MultiSelectList
            return companySelectList;
        }

        private MultiSelectList GetAllMajorsMultiSelectList()
        {
            //Get the list of majors from the database
            List<Major> majorList = _context.Majors.ToList();

            //add a dummy entry so the user can select all majors
            Major SelectNone = new Major() { MajorId = 0, MajorName = "All Majors" };
            majorList.Add(SelectNone);

            //convert the list to a SelectList by calling SelectList constructor
            MultiSelectList majorSelectList = new MultiSelectList(majorList.OrderBy(m => m.MajorId), "MajorId", "MajorName");

            //return the MultiSelectList
            return majorSelectList;
        }

        private MultiSelectList GetAllMajorsMultiSelectList(Position position)
        {
            //create a list of all the suppliers
            List<Major> allMajors = _context.Majors.ToList();

            //convert product.Suppliers to a list of ids
            List<Int32> associatedMajorIDs = new List<Int32>();

            foreach (Major major in position.Majors)
            {
                associatedMajorIDs.Add(major.MajorId);
            }

            //create the multi-select list
            MultiSelectList msl = new MultiSelectList(allMajors.OrderBy(m => m.MajorId), nameof(Major.MajorId), nameof(Major.MajorName), associatedMajorIDs);

            //return the multiselect list
            return msl;
        }

        public async Task<IActionResult> DisplaySearchResults(PositionSearchViewModel psvm)
        {
            // LINQ query to filter books
            var query = from p in _context.Positions select p;

            ViewBag.AllPositions = _context.Positions.Count();

            if (User.IsInRole("Student") == true)
            {
                ISession session = HttpContext.Session;
                string dateString = session.GetString("AppDate");
                DateTime AppDate = DateTime.Parse(dateString);
                query = query.Where(p => p.Deadline >= AppDate);

                ViewBag.AllPositions = query.Count();
            }

            if (User.IsInRole("Recruiter") == true)
            {
                AppUser recruiter = await _context.Users.Include(u => u.Company)
                                                            .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

                query = query.Where(p => p.Company == recruiter.Company);
                ViewBag.AllPositions = query.Count();
            }

            if (psvm.PositionTitle != "" && psvm.PositionTitle != null)
            {
                query = query.Where(p => p.PositionTitle.Contains(psvm.PositionTitle) || p.PositionDescription.Contains(psvm.PositionTitle));
            }

            if (psvm.Location != "" && psvm.Location != null)
            {
                query = query.Where(p => p.Location.Contains(psvm.Location));
            }

            if (psvm.Industry != null)
            {
                query = query.Where(p => p.Company.Industry1 == psvm.Industry || p.Company.Industry2 == psvm.Industry || p.Company.Industry3 == psvm.Industry);
            }

            if (psvm.PositionType != null)
            {
                query = query.Where(p => p.PositionType == psvm.PositionType);
            }

            if (psvm.CompanyIDs != null && !psvm.CompanyIDs.Contains(0))
            {
                query = query.Where(p => psvm.CompanyIDs.Contains(p.Company.CompanyID));
            }

            /*if (psvm.CompanyID != 0)
            {
                query = query.Where(p => p.Company.CompanyID == psvm.CompanyID);
            }*/

            /*if (psvm.MajorID != 0)
            {
                query = query.Where(p => p.Majors.Select(m => m.MajorId).Contains(psvm.MajorID));
            }*/

            if (psvm.MajorIDs != null && !psvm.MajorIDs.Contains(0))
            {
                query = query.Where(p => p.Majors.Any(m => psvm.MajorIDs.Contains(m.MajorId)));
            }

            // select all books in query and return view with books.
            List<Position> SelectedPositions = query.Include(p => p.Company).Include(p => p.Majors).ToList();

            ViewBag.SelectedPositions = SelectedPositions.Count;

            return View("Index", SelectedPositions.OrderBy(p => p.Deadline));
        }


        // GET: Positions/Invite
        public async Task<IActionResult> Invite(int id)
        {
            //var applications = await _context.Applications.FirstOrDefaultAsync();
            var query = from a in _context.Applications select a;
            ViewBag.AllStudents = GetAllStudentsSelectList();
            ViewBag.AllPositions = await GetAllPositionsSelectListAsync();

            if (User.IsInRole("Recruiter") == true)
            {
                // find recruiter
                Position position = _context.Positions.FirstOrDefault(p => p.PositionID == id);
                // restrict query to only application for positions in rec's company
                query = query.Where(a => a.Position == position);

                // recruiters can only see application for positions that have passed deadine
                ISession session = HttpContext.Session;
                string dateString = session.GetString("AppDate");
                DateTime AppDate = DateTime.Parse(dateString);

                query = query.Where(a => a.Position.Deadline <= AppDate);

            }

            List<Application> SelectedApplications = await query.Include(a => a.Position)
                                                                .ThenInclude(p => p.Company)
                                                                .Include(a => a.Student).ToListAsync();

            List<AppUser> SelectedStudents = SelectedApplications.Select(a => a.Student).ToList();


            return Invite(id, SelectedStudents);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: Positions/Invite
        public IActionResult Invite(int id, List<AppUser> SelectedStudents)
        {
            for (int i = 0; i < SelectedStudents.Count; i++)
            {
                AppUser student = SelectedStudents[i];
                var query = from a in _context.Applications select a;
                Application application = (Application)query.Where(a => a.Student == student).Where(a => a.Position.PositionID == id);

                application.AppStatus = Status.Accepted;
            }

            //Utilities.EmailMessaging.SendEmail("Interview Invitation", "Congrats! You have been invited to an interview. Please proceed to your application portal to view and accept");

            var fromAddress = new MailAddress("sp23Team33Kproject@gmail.com", "Team 33");
            var toAddress = new MailAddress("sp23Team33Kproject@gmail.com", "Recipient");
            const string fromPassword = "xxoqxnjriwtzpfhj";
            const string subject = "Interview Invitation";
            const string body = "Congrats! You have been invited to an interview. Please proceed to your application portal to view and accept";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com", // replace with your SMTP server's host name
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }




            return View();
        }



        // GET: Positions/RecruiterIndex
        public async Task<IActionResult> RecruiterIndex()
        {

            if (User.IsInRole("Recruiter"))
            {
                //AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

                AppUser appUser = await _context.Users.Include(u => u.Company).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

                var appDbContext = _context.Positions.Include(i => i.Company)
                                                       .Where(i => i.Company.CompanyName == appUser.Company.CompanyName);
                return View(await appDbContext.ToListAsync());
            }

            else
            {
                return View();
            }
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

        private async Task<SelectList> GetAllPositionsSelectListAsync()
        {
            //Get the list of majors from the database
            List<Position> positionList = _context.Positions.ToList();
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            //convert the list to a SelectList by calling SelectList constructor
            SelectList positionSelectList = new SelectList(positionList.OrderBy(m => m.PositionID).Where(m => m.Company == appUser.Company), "PositionID", "PositionName");

            //return the MultiSelectList
            return positionSelectList;
        }

    }
}