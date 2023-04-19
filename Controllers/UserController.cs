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
    public class UserController : Controller
    {
        private readonly EmployeeDbContext _context;

        public UserController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
              return _context.UserTables != null ? 
                          View(await _context.UserTables.ToListAsync()) :
                          Problem("Entity set 'EmployeeDbContext.UserTables'  is null.");
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserTables == null)
            {
                return NotFound();
            }

            var userTable = await _context.UserTables
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userTable == null)
            {
                return NotFound();
            }

            return View(userTable);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserName,Password,Role")] UserTable userTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userTable);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserTables == null)
            {
                return NotFound();
            }

            var userTable = await _context.UserTables.FindAsync(id);
            if (userTable == null)
            {
                return NotFound();
            }
            return View(userTable);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserName,Password,Role")] UserTable userTable)
        {
            if (id != userTable.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTableExists(userTable.UserId))
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
            return View(userTable);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserTables == null)
            {
                return NotFound();
            }

            var userTable = await _context.UserTables
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userTable == null)
            {
                return NotFound();
            }

            return View(userTable);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserTables == null)
            {
                return Problem("Entity set 'EmployeeDbContext.UserTables'  is null.");
            }
            var userTable = await _context.UserTables.FindAsync(id);
            if (userTable != null)
            {
                _context.UserTables.Remove(userTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserTableExists(int id)
        {
          return (_context.UserTables?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
        public ActionResult login () { return View(); }
        [HttpPost]

        public ActionResult login (string userName, string password)
        {
            var status = _context.UserTables.Where(m => m.UserName == userName && m.Password == password).FirstOrDefault();
            if (status == null) 
            {
                return View(); // which means there us no user and we will stay on the same page.
            }
            if (status.Role == "Admin")
            {
                return (RedirectToAction("Index", "Home"));
            }
            else
            {
                return (RedirectToAction("Index", "employeeDashboard"));
            } 
        }
    }
}
