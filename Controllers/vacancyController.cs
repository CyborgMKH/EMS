using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EMS.Models;

namespace EMS.Controllers
{
    public class vacancyController : Controller
    {
        private readonly EmployeeDbContext _context;

        public vacancyController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: vacancy
        public async Task<IActionResult> Index()
        {
            var employeeDbContext = _context.VacancyTables.Include(v => v.Department).Include(v => v.Designation);
            return View(await employeeDbContext.ToListAsync());
        }

        // GET: vacancy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VacancyTables == null)
            {
                return NotFound();
            }

            var vacancyTable = await _context.VacancyTables
                .Include(v => v.Department)
                .Include(v => v.Designation)
                .FirstOrDefaultAsync(m => m.VacancyId == id);
            if (vacancyTable == null)
            {
                return NotFound();
            }

            return View(vacancyTable);
        }

        // GET: vacancy/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.DepartmentTables, "DepartmentId","DepartmentName" , "DepartmentId");
            ViewData["DesignationId"] = new SelectList(_context.DesignationTables, "DesignationId","DesignationName" , "DesignationId");
            return View();
        }

        // POST: vacancy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VacancyId,DepartmentId,DesignationId,VacancyFrom,VacancyTo")] VacancyTable vacancyTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vacancyTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.DepartmentTables, "DepartmentId", "DepartmentId", vacancyTable.DepartmentId);
            ViewData["DesignationId"] = new SelectList(_context.DesignationTables, "DesignationId", "DesignationId", vacancyTable.DesignationId);
            return View(vacancyTable);
        }

        // GET: vacancy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VacancyTables == null)
            {
                return NotFound();
            }

            var vacancyTable = await _context.VacancyTables.FindAsync(id);
            if (vacancyTable == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.DepartmentTables, "DepartmentId", "DepartmentId", vacancyTable.DepartmentId);
            ViewData["DesignationId"] = new SelectList(_context.DesignationTables, "DesignationId", "DesignationId", vacancyTable.DesignationId);
            return View(vacancyTable);
        }

        // POST: vacancy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VacancyId,DepartmentId,DesignationId,VacancyFrom,VacancyTo")] VacancyTable vacancyTable)
        {
            if (id != vacancyTable.VacancyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacancyTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacancyTableExists(vacancyTable.VacancyId))
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
            ViewData["DepartmentId"] = new SelectList(_context.DepartmentTables, "DepartmentId", "DepartmentId", vacancyTable.DepartmentId);
            ViewData["DesignationId"] = new SelectList(_context.DesignationTables, "DesignationId", "DesignationId", vacancyTable.DesignationId);
            return View(vacancyTable);
        }

        // GET: vacancy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VacancyTables == null)
            {
                return NotFound();
            }

            var vacancyTable = await _context.VacancyTables
                .Include(v => v.Department)
                .Include(v => v.Designation)
                .FirstOrDefaultAsync(m => m.VacancyId == id);
            if (vacancyTable == null)
            {
                return NotFound();
            }

            return View(vacancyTable);
        }

        // POST: vacancy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VacancyTables == null)
            {
                return Problem("Entity set 'EmployeeDbContext.VacancyTables'  is null.");
            }
            var vacancyTable = await _context.VacancyTables.FindAsync(id);
            if (vacancyTable != null)
            {
                _context.VacancyTables.Remove(vacancyTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacancyTableExists(int id)
        {
          return (_context.VacancyTables?.Any(e => e.VacancyId == id)).GetValueOrDefault();
        }
    }
}
