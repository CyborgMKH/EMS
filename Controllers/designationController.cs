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
    public class designationController : Controller
    {
        private readonly EmployeeDbContext _context;

        public designationController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: designation
        public async Task<IActionResult> Index()
        {
              return _context.DesignationTables != null ? 
                          View(await _context.DesignationTables.ToListAsync()) :
                          Problem("Entity set 'EmployeeDbContext.DesignationTables'  is null.");
        }

        // GET: designation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DesignationTables == null)
            {
                return NotFound();
            }

            var designationTable = await _context.DesignationTables
                .FirstOrDefaultAsync(m => m.DesignationId == id);
            if (designationTable == null)
            {
                return NotFound();
            }

            return View(designationTable);
        }

        // GET: designation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: designation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DesignationId,DesignationName,Description")] DesignationTable designationTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(designationTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(designationTable);
        }

        // GET: designation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DesignationTables == null)
            {
                return NotFound();
            }

            var designationTable = await _context.DesignationTables.FindAsync(id);
            if (designationTable == null)
            {
                return NotFound();
            }
            return View(designationTable);
        }

        // POST: designation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DesignationId,DesignationName,Description")] DesignationTable designationTable)
        {
            if (id != designationTable.DesignationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(designationTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DesignationTableExists(designationTable.DesignationId))
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
            return View(designationTable);
        }

        // GET: designation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DesignationTables == null)
            {
                return NotFound();
            }

            var designationTable = await _context.DesignationTables
                .FirstOrDefaultAsync(m => m.DesignationId == id);
            if (designationTable == null)
            {
                return NotFound();
            }

            return View(designationTable);
        }

        // POST: designation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DesignationTables == null)
            {
                return Problem("Entity set 'EmployeeDbContext.DesignationTables'  is null.");
            }
            var designationTable = await _context.DesignationTables.FindAsync(id);
            if (designationTable != null)
            {
                _context.DesignationTables.Remove(designationTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DesignationTableExists(int id)
        {
          return (_context.DesignationTables?.Any(e => e.DesignationId == id)).GetValueOrDefault();
        }
    }
}
