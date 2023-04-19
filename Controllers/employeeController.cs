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
    public class employeeController : Controller
    {
        private readonly EmployeeDbContext _context;

        public employeeController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: employee
        public async Task<IActionResult> Index()
        {
            var employeeDbContext = _context.EmployeeTables.Include(e => e.Department).Include(e => e.Designation);
            return View(await employeeDbContext.ToListAsync());
        }

        // GET: employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmployeeTables == null)
            {
                return NotFound();
            }

            var employeeTable = await _context.EmployeeTables
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employeeTable == null)
            {
                return NotFound();
            }

            return View(employeeTable);
        }

        // GET: employee/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.DepartmentTables, "DepartmentId", "DepartmentName","DepartmentId");
            //ViewData["DepartmentId"] = new SelectList(_context.DepartmentTables, "DepartmentId", "DepartmentId");
            ViewData["DesignationId"] = new SelectList(_context.DesignationTables, "DesignationId","DesignationName", "DesignationId");
            return View();
        }

        // POST: employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,DepartmentId,DesignationId,EmployeeName,Dob,Address,Contact,Email,Salary")] EmployeeTable employeeTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.DepartmentTables, "DepartmentId", "DepartmentId", employeeTable.DepartmentId);
            ViewData["DesignationId"] = new SelectList(_context.DesignationTables, "DesignationId", "DesignationId", employeeTable.DesignationId);
            return View(employeeTable);
        }

        // GET: employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmployeeTables == null)
            {
                return NotFound();
            }

            var employeeTable = await _context.EmployeeTables.FindAsync(id);
            if (employeeTable == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.DepartmentTables, "DepartmentId", "DepartmentId", employeeTable.DepartmentId);
            ViewData["DesignationId"] = new SelectList(_context.DesignationTables, "DesignationId", "DesignationId", employeeTable.DesignationId);
            return View(employeeTable);
        }

        // POST: employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,DepartmentId,DesignationId,EmployeeName,Dob,Address,Contact,Email,Salary")] EmployeeTable employeeTable)
        {
            if (id != employeeTable.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeTableExists(employeeTable.EmployeeId))
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
            ViewData["DepartmentId"] = new SelectList(_context.DepartmentTables, "DepartmentId", "DepartmentId", employeeTable.DepartmentId);
            ViewData["DesignationId"] = new SelectList(_context.DesignationTables, "DesignationId", "DesignationId", employeeTable.DesignationId);
            return View(employeeTable);
        }

        // GET: employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmployeeTables == null)
            {
                return NotFound();
            }

            var employeeTable = await _context.EmployeeTables
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employeeTable == null)
            {
                return NotFound();
            }

            return View(employeeTable);
        }

        // POST: employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmployeeTables == null)
            {
                return Problem("Entity set 'EmployeeDbContext.EmployeeTables'  is null.");
            }
            var employeeTable = await _context.EmployeeTables.FindAsync(id);
            if (employeeTable != null)
            {
                _context.EmployeeTables.Remove(employeeTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeTableExists(int id)
        {
          return (_context.EmployeeTables?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
