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
using Microsoft.CodeAnalysis;

namespace Ispit.Todo.Controllers
{
    [Authorize]
    public class AspNetUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AspNetUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AspNetUsers
        public async Task<IActionResult> Index()
        {
            if( _context.AspNetUser == null)
            {
               return  Problem("Entity set 'ApplicationDbContext.AspNetUser'  is null.");

            }
            var users =await _context.AspNetUser.ToListAsync();
                        
           
            foreach (var user in users)
            {
               
                user.Todolists = _context.Todolist.Where(pc => pc.TodoId == user.AspNetId).ToList();
            }
            ViewBag.Todolists = _context.Todolist.ToList();
            return View(users);
        }
    

// GET: AspNetUsers/Details/5
public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AspNetUser == null)
            {
                return NotFound();
            }

            var aspNetUser = await _context.AspNetUser
                .FirstOrDefaultAsync(m => m.AspNetUserId == id);
            if (aspNetUser == null)
            {
                return NotFound();
            }

            return View(aspNetUser);
        }

        // GET: AspNetUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AspNetUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AspNetUserId,FirstName,LastName,Address")] AspNetUser aspNetUser)
        {
            ModelState.Remove("Todolists");
            if (ModelState.IsValid)
            {
                _context.Add(aspNetUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aspNetUser);
        }

        // GET: AspNetUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AspNetUser == null)
            {
                return NotFound();
            }

            var aspNetUser = await _context.AspNetUser.FindAsync(id);
            if (aspNetUser == null)
            {
                return NotFound();
            }
            aspNetUser.Todolists = _context.Todolist.Where(pc => pc.TodoId == aspNetUser.AspNetId).ToList();
            ViewBag.Todolists = _context.Todolist.ToList();
            return View(aspNetUser);
        }

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AspNetUserId,FirstName,LastName,Address")] AspNetUser aspNetUser)
        {
            if (id != aspNetUser.AspNetUserId)
            {
                return NotFound();
            }
            ModelState.Remove("Todolists");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aspNetUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspNetUserExists(aspNetUser.AspNetUserId))
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
            return View(aspNetUser);
        }
        [HttpPost]

        public async Task<IActionResult> EditTodo(int id)
        {
            List<Todolist> todos = _context.Todolist.ToList();
            foreach (var cat in todos)
            {
                var checkbox = Request.Form[cat.TodoTitle];
                if (checkbox.Contains("true"))
                {
                    if (_context.Todolist.FirstOrDefault(x => x.TodoId == id)==null)
                    { 

                        
                        _context.Todolist.Add(new Todolist() { TodoId = cat.Id } );
                    }

                }
                else
                {
                    var p = _context.Todolist.FirstOrDefault(x => x.TodoId == cat.Id); 
                    if (p != null) 
                    {
                        _context.Todolist.Remove(p);
                    }
                }
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Edit), new { id });

        }
        // GET: AspNetUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AspNetUser == null)
            {
                return NotFound();
            }

            var aspNetUser = await _context.AspNetUser
                .FirstOrDefaultAsync(m => m.AspNetUserId == id);
            if (aspNetUser == null)
            {
                return NotFound();
            }

            return View(aspNetUser);
        }

        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AspNetUser == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AspNetUser'  is null.");
            }
            var aspNetUser = await _context.AspNetUser.FindAsync(id);
            if (aspNetUser != null)
            {
                _context.AspNetUser.Remove(aspNetUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetUserExists(int id)
        {
          return (_context.AspNetUser?.Any(e => e.AspNetUserId == id)).GetValueOrDefault();
        }
    }
}
