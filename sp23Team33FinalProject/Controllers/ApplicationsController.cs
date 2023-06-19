using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ApplicationsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ApplicationsController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            //var applications = await _context.Applications.FirstOrDefaultAsync();
            var query = from a in _context.Applications select a;
            ViewBag.AllStudents = GetAllStudentsSelectList();
            ViewBag.AllPositions = await GetAllPositionsSelectListAsync();

            if (User.IsInRole("Student") == true)
            {
                query = query.Where(a => a.Student.UserName == User.Identity.Name);
            }

            if (User.IsInRole("Recruiter") == true)
            {
                // find recruiter
                AppUser recruiter = await _context.Users.Include(u => u.Company)
                    .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                // restrict query to only application for positions in rec's company
                query = query.Where(a => a.Position.Company == recruiter.Company);

                // recruiters can only see application for positions that have passed deadine
                ISession session = HttpContext.Session;
                string dateString = session.GetString("AppDate");
                DateTime AppDate = DateTime.Parse(dateString);

                query = query.Where(a => a.Position.Deadline <= AppDate);

            }

            List<Application> SelectedApplications = await query.Include(a => a.Position)
                                                                .ThenInclude(p => p.Company)
                                                                .Include(a => a.Student).ToListAsync();

            return View(SelectedApplications.OrderBy(a => a.Position.Deadline));
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return View("Error", new String[] { "Please specify a application to view!" });
            }

            var application = await _context.Applications
                .Include(a => a.Position)
                .ThenInclude(a => a.Company)
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.ApplicationID == id);

            if (application == null)
            {
                return View("Error", new String[] { "This application was not found in the database!" });
            }

            return View(application);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationID,AppStatus")] Application application)
        {
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(application);
        }

        // GET: Applications/Edit/5
        [Authorize(Roles = "Recruiter, CSO")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationID,AppStatus")] Application application)
        {
            if (id != application.ApplicationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.ApplicationID))
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
            return View(application);
        }

        // GET: Applications/Delete/5
        [Authorize(Roles = "CSO, Student")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.Include(a => a.Position)
                .FirstOrDefaultAsync(m => m.ApplicationID == id);
            if (application == null)
            {
                return NotFound();
            }

            if(User.IsInRole("Student")==true)
            {
                ISession session = HttpContext.Session;
                string dateString = session.GetString("AppDate");
                DateTime AppDate = DateTime.Parse(dateString);

                if(application.Position.Deadline <= AppDate)
                {
                    return View("Error", new String[] { "Deadline has passed, cannot withdraw application!" });
                }
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        [Authorize(Roles = "CSO, Student")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Applications == null)
            {
                return Problem("Entity set 'AppDbContext.Applications'  is null.");
            }
            var application = await _context.Applications.FindAsync(id);
            if (application != null)
            {
                _context.Applications.Remove(application);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // TODO add authroize tag
        // GET: Applications/Create
        public async Task<IActionResult> Invite(int? id)
        {
            ViewBag.AllStudents = GetAllStudentsSelectList();
            //  ViewBag.AllPositions = await GetAllPositionsSelectListAsync();
            return View();
        }

        //POST: Applications/Invite
        //To protect from overposting attacks, enable the specific properties you want to bind to.
        //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        //TODO add authroize tag

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Invite(AppUser Student, [Bind("Student,Position")] Application application)
        {
            ViewBag.AllStudents = GetAllStudentsSelectList();
            if (ModelState.IsValid)
            {
                application.AppStatus = Status.Accepted;
                application.Student = (AppUser)_context.Users.Where(p => p == Student);

                //add the order detail to the database
                _context.Applications.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("RecruiterIndex", "Positions");
        }







        private bool ApplicationExists(int id)
        {
          return (_context.Applications?.Any(e => e.ApplicationID == id)).GetValueOrDefault();
        }
        public IActionResult Confirm()
        {
            return View();
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
