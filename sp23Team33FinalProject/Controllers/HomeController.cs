using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sp23Team33FinalProject.DAL;
using sp23Team33FinalProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace sp23Team33FinalProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [AllowAnonymous]
        public IActionResult SetDate()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SetDate(SetDateViewModel sdvm)
        {
            if (sdvm == null || sdvm.SetDate == null)
            {
                return View("Error", new String[] { "Please specify a new date to set!" });
            }

            ISession session = HttpContext.Session;
            // Store the current date in the session as a string
            session.SetString("AppDate", sdvm.SetDate.ToString());

            // Retrieve the date from the session and convert it back to a DateTime object
            string dateString = session.GetString("AppDate");
            DateTime AppDate = DateTime.Parse(dateString);
            ViewBag.CurrentDate = AppDate;

            return View("Index");
        }

        [AllowAnonymous]
        public IActionResult Index()
        {

            ISession session = HttpContext.Session;
            if (session.GetString("AppDate") == null)
            {
                session.SetString("AppDate", DateTime.Now.ToString());
            }

            string dateString = session.GetString("AppDate");
            DateTime AppDate = DateTime.Parse(dateString);

            ViewBag.CurrentDate = AppDate;

            return View();
        }

        [Authorize(Roles="CSO, Recruiter")]
        public IActionResult StudentsIndex()
        {
            var query = from s in _context.Users select s;
            // filter to only include students
            query = query.Where(s => s.GraduationYear != null);

            ViewBag.AllStudents = query.Count();
            ViewBag.SelectedStudents = query.Count();

            // selects all positions in query
            List<AppUser> SelectedStudents = query.Include(s => s.Major).ToList();

            return View(SelectedStudents.OrderBy(s => s.FirstName));
        }

        [Authorize(Roles = "CSO")]
        public async Task<IActionResult> RecruitersIndex()
        {
            // Get all users with the "recruiter" role
            var recruiters = await _userManager.GetUsersInRoleAsync("Recruiter");

            // Select all recruiters and their companies
            var query = from r in _context.Users.Where(u => recruiters.Contains(u)) select r;
            List<AppUser> SelectedRecruiters = query.Include(r => r.Company).ToList();

            ViewBag.AllRecruiters = query.Count();
            ViewBag.SelectedRecruiters = query.Count();

            return View(SelectedRecruiters.OrderBy(r => r.FirstName));
        }

        [Authorize(Roles = "CSO")]
        public async Task<IActionResult> CSOIndex()
        {
            // Get all users with the "recruiter" role
            var CSOs = await _userManager.GetUsersInRoleAsync("CSO");

            // Select all recruiters and their companies
            var query = from c in _context.Users.Where(u => CSOs.Contains(u)) select c;
            List<AppUser> SelectedCSOs = query.Include(c => c.Company).ToList();

            return View(SelectedCSOs.OrderBy(c => c.FirstName));
        }

        [Authorize(Roles = "CSO, Recruiter")]
        public IActionResult StudentSearch()
        {
            ViewBag.AllMajors = GetAllMajorsMultiSelectList();
            return View();
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

        [Authorize(Roles = "CSO, Recruiter")]
        public IActionResult DisplayStudentSearchResults(StudentSearchViewModel ssvm)
        {
            // LINQ query to filter books
            var query = from s in _context.Users select s;
            query = query.Where(s => s.GraduationYear != null);
            ViewBag.AllStudents = query.Count();

            //TODO: Check to make sure user is Student
            if (ssvm.StudentName != "" && ssvm.StudentName != null)
            {
                query = query.Where(s => (s.FirstName+s.LastName).Contains(ssvm.StudentName));
            }
            
            if (ssvm.GradYear != null)
            {
                query = query.Where(s => s.GraduationYear == ssvm.GradYear.ToString().Substring(1));

            }

            if (ssvm.MajorIDs != null && !ssvm.MajorIDs.Contains(0))
            {
                query = query.Where(s => ssvm.MajorIDs.Contains(s.Major.MajorId));
            }

            if (ssvm.GPA != null)
            {
                if (ssvm.GPASearchType == GPASearchType.LessThan)
                {
                    query = query.Where(s => s.GPA <= ssvm.GPA);
                }
                else if (ssvm.GPASearchType == GPASearchType.GreaterThan)
                {
                    query = query.Where(s => s.GPA >= ssvm.GPA);
                }
            }

            if (ssvm.PositionType != null)
            {
                query = query.Where(s => s.PositionType == ssvm.PositionType);
            }

            // select all books in query and return view with books.
            List<AppUser> SelectedStudents = query.Include(s => s.Major).ToList();
            
            ViewBag.SelectedStudents = SelectedStudents.Count();

            return View("StudentsIndex", SelectedStudents.OrderBy(s => s.FirstName));
        }

        [Authorize(Roles = "CSO")]
        public IActionResult RecruiterSearch()
        {
            ViewBag.AllCompanies = GetAllCompaniesMultiSelectList();
            return View();

        }
        private MultiSelectList GetAllCompaniesMultiSelectList()
        {
            //Get the list of companies from the database
            List<Company> companyList = _context.Companies.ToList();

            //add a dummy entry so the user can select all majors
            Company SelectNone = new Company() { CompanyID = 0, CompanyName = "All Companies!" };
            companyList.Add(SelectNone);

            //convert the list to a SelectList by calling SelectList constructor
            MultiSelectList companySelectList = new MultiSelectList(companyList.OrderBy(c => c.CompanyID), "CompanyID", "CompanyName");

            //return the MultiSelectList
            return companySelectList;
        }

        [Authorize(Roles = "CSO")]
        public async Task<IActionResult> DisplayRecruiterSearchResults(RecruiterSearchViewModel rsvm)
        {
            // Select all recruiters and their companies
            var recruiters = await _userManager.GetUsersInRoleAsync("Recruiter");
            var query = from r in _context.Users.Where(u => recruiters.Contains(u)) select r;
            ViewBag.AllRecruiters = query.Count();

            if (rsvm.RecruiterName != "" && rsvm.RecruiterName != null)
            {
                query = query.Where(r => (r.FirstName + r.LastName).Contains(rsvm.RecruiterName));
            }

            if (rsvm.CompanyIDs != null && !rsvm.CompanyIDs.Contains(0))
            {
                query = query.Where(r => rsvm.CompanyIDs.Contains(r.Company.CompanyID));
            }

            // select all books in query and return view with books.
            List<AppUser> SelectedRecruiters = query.Include(r => r.Company).ToList();

            ViewBag.SelectedRecruiters = SelectedRecruiters.Count;

            return View("RecruitersIndex", SelectedRecruiters.OrderBy(s => s.FirstName));
        }
    }
}
