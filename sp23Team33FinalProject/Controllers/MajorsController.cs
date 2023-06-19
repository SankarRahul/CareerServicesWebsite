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

namespace sp23Team33FinalProject.Controllers
{
    [Authorize]
    public class MajorsController : Controller
    {
        private readonly AppDbContext _context;

        public MajorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Majors
        public async Task<IActionResult> Index()
        {
              return _context.Majors != null ? 
                          View(await _context.Majors.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Majors'  is null.");
        }

        // GET: Majors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Majors == null)
            {
                return NotFound();
            }

            var major = await _context.Majors
                .FirstOrDefaultAsync(m => m.MajorId == id);
            if (major == null)
            {
                return NotFound();
            }

            return View(major);
        }

        [Authorize(Roles = "CSO")]
        // GET: Majors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Majors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "CSO")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MajorId,MajorName")] Major major)
        {
            if (ModelState.IsValid)
            {
                _context.Add(major);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(major);
        }

        [Authorize(Roles = "CSO")]
        // GET: Majors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Majors == null)
            {
                return NotFound();
            }

            var major = await _context.Majors.FindAsync(id);
            if (major == null)
            {
                return NotFound();
            }
            return View(major);
        }

        // POST: Majors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CSO")]
        public async Task<IActionResult> Edit(int id, [Bind("MajorId,MajorName")] Major major)
        {
            if (id != major.MajorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(major);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MajorExists(major.MajorId))
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
            return View(major);
        }

        private bool MajorExists(int id)
        {
          return (_context.Majors?.Any(e => e.MajorId == id)).GetValueOrDefault();
        }
    }
}
