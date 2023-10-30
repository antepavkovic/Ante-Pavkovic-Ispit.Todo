using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ispit.Todo.Data;
using Ispit.Todo.Models;
using Microsoft.AspNetCore.Authorization;

namespace Ispit.Todo.Controllers
{
    [Authorize]
    public class Task1Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public Task1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Task1
        public async Task<IActionResult> Index()
        {
              return _context.Task1 != null ? 
                          View(await _context.Task1.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Task1'  is null.");
        }

        // GET: Task1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Task1 == null)
            {
                return NotFound();
            }

            var task1 = await _context.Task1
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task1 == null)
            {
                return NotFound();
            }

            return View(task1);
        }

        // GET: Task1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Task1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Status,TaskTitle")] Task1 task1)
        {
            ModelState.Remove("");
            if (ModelState.IsValid)
            {
                _context.Add(task1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(task1);
        }

        // GET: Task1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Task1 == null)
            {
                return NotFound();
            }

            var task1 = await _context.Task1.FindAsync(id);
            if (task1 == null)
            {
                return NotFound();
            }
            return View(task1);
        }

        // POST: Task1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status,TaskTitle")] Task1 task1)
        {
            if (id != task1.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Task1Exists(task1.Id))
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
            return View(task1);
        }

        // GET: Task1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Task1 == null)
            {
                return NotFound();
            }

            var task1 = await _context.Task1
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task1 == null)
            {
                return NotFound();
            }

            return View(task1);
        }

        // POST: Task1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Task1 == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Task1'  is null.");
            }
            var task1 = await _context.Task1.FindAsync(id);
            if (task1 != null)
            {
                _context.Task1.Remove(task1);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Task1Exists(int id)
        {
          return (_context.Task1?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
