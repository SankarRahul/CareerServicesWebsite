using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sp23Team33FinalProject.DAL;
using sp23Team33FinalProject.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace sp23Team33FinalProject.Controllers
{
    [Authorize]
    public class CompaniesController : Controller
    {
        private readonly AppDbContext _context;

        public CompaniesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            var query = from c in _context.Companies select c;
            ViewBag.AllCompanies = query.Count();
            // TODO restrict recuiters from changing other company details
            if (User.IsInRole("Recruiter") == true)
            {
                AppUser recruiter = await _context.Users.Include(u => u.Company)
                                                            .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

                query = query.Where(p => p.Recruiters.Contains(recruiter));
            }

            List<Company> SelectedCompanies = query.Include(c => c.Positions).ToList();

            ViewBag.SelectedCompanies = SelectedCompanies.Count();

            return View(SelectedCompanies.OrderBy(c => c.CompanyName));
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var query = from a in _context.Companies select a;
            if (id == null || _context.Companies == null)
            {
                return View("Error", new String[] { "Please specify a company to view details!" });
            }

            var company = await _context.Companies.Include(c => c.Positions)
                .FirstOrDefaultAsync(m => m.CompanyID == id);

            if (company == null)
            {
                return View("Error", new String[] { "This company does not exist in the database!" });
            }

            

                return View(company);
        }

        // GET: Companies/Create
        [Authorize(Roles = "Recruiter, CSO")]
        public async Task<IActionResult> Create()
        {
            if(User.IsInRole("Recruiter"))
            {
                AppUser recruiter = await _context.Users.Include(u => u.Company).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                if(recruiter.Company != null)
                {
                    return View("Error", new String[] { "You are NOT authorized to create companies!" });
                }
            }
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Recruiter, CSO")]
        public async Task<IActionResult> Create([Bind("CompanyID,CompanyName,CompanyDesc,CompanyEmail,Industry1,Industry2,Industry3")] Company company)
        {
            if (User.IsInRole("Recruiter"))
            {
                AppUser recruiter = await _context.Users.Include(u => u.Company).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
                if (recruiter.Company != null)
                {
                    return View("Error", new String[] { "You are NOT authorized to create companies!" });
                }
                if (ModelState.IsValid)
                {
                    _context.Add(company);
                    recruiter.Company = company;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        [Authorize(Roles = "Recruiter, CSO")]
        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Recruiter, CSO")]
        public async Task<IActionResult> Edit(int id, [Bind("CompanyID,CompanyName,CompanyDesc,CompanyEmail,Industry1,Industry2,Industry3")] Company company)
        {
            if (id != company.CompanyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.CompanyID))
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
            return View(company);
        }

        private bool CompanyExists(int id)
        {
          return (_context.Companies?.Any(e => e.CompanyID == id)).GetValueOrDefault();
        }

        public IActionResult CompanySearch()
        {
            return View();
        }

        public IActionResult DisplayCompanySearchResults(CompanySearchViewModel csvm)
        {
            // LINQ query to filter books
            var query = from c in _context.Companies select c;

            if (csvm.CompanyName != "" && csvm.CompanyName != null)
            {
                query = query.Where(c => c.CompanyName.Contains(csvm.CompanyName));
            }

            if (csvm.Location != "" && csvm.Location != null)
            {
                query = query.Where(c => c.Positions.Any(p => p.Location.Contains(csvm.Location)));
            }

            if (csvm.Industry != null)
            {
                query = query.Where(c => c.Industry1 == csvm.Industry || c.Industry2 == csvm.Industry || c.Industry3 == csvm.Industry);
            }

            if (csvm.PositionType != null)
            {
                query = query.Where(c => c.Positions.Any(p => p.PositionType == csvm.PositionType));
            }

            // select all books in query and return view with books.
            List<Company> SelectedCompanies = query.Include(c => c.Positions).ToList();

            return View("Index", SelectedCompanies.OrderBy(c => c.CompanyName));
        }
    }
}
