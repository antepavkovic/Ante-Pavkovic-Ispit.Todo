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
    public class TodolistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TodolistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Todolists
     
        public async Task<IActionResult> Index()
        {
            if (_context.Todolist == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AspNetUser'  is null.");

            }
            var todolists = await _context.Todolist.ToListAsync();


            foreach (var tdlist in todolists)
            {

                tdlist.Tasks1 = _context.Task1.Where(pc => pc.TaskId == tdlist.Id).ToList();
            }
            ViewBag.Tasks1 = _context.Task1.ToList();
            return View(todolists);
        }
        // GET: Todolists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Todolist == null)
            {
                return NotFound();
            }

            var todolist = await _context.Todolist
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todolist == null)
            {
                return NotFound();
            }

            return View(todolist);
        }

        // GET: Todolists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Todolists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TodoTitle,Description")] Todolist todolist)
        {
            ModelState.Remove("Tasks1");
            if (ModelState.IsValid)
            {
                _context.Add(todolist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todolist);
        }

        // GET: Todolists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Todolist == null)
            {
                return NotFound();
            }

            var todolist = await _context.Todolist.FindAsync(id);
            if (todolist == null)
            {
                return NotFound();
            }
            return View(todolist);
        }

        // POST: Todolists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TodoTitle,Description")] Todolist todolist)
        {
            if (id != todolist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todolist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodolistExists(todolist.Id))
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
            return View(todolist);
        }

        // GET: Todolists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Todolist == null)
            {
                return NotFound();
            }

            var todolist = await _context.Todolist
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todolist == null)
            {
                return NotFound();
            }

            return View(todolist);
        }

        // POST: Todolists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Todolist == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Todolist'  is null.");
            }
            var todolist = await _context.Todolist.FindAsync(id);
            if (todolist != null)
            {
                _context.Todolist.Remove(todolist);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodolistExists(int id)
        {
          return (_context.Todolist?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
