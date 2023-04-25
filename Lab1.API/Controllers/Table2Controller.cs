using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab1.API.DBContext;
using Lab1.API.Entities;

namespace Lab1.API.Controllers
{
    public class Table2Controller : Controller
    {
        private readonly DataBaseContext _context;

        public Table2Controller(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Table2
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.table2.Include(t => t.Table1);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: Table2/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.table2 == null)
            {
                return NotFound();
            }

            var table2 = await _context.table2
                .Include(t => t.Table1)
                .FirstOrDefaultAsync(m => m.id == id);
            if (table2 == null)
            {
                return NotFound();
            }

            return View(table2);
        }

        // GET: Table2/Create
        public IActionResult Create()
        {
            ViewData["Table1Id"] = new SelectList(_context.table1, "Id", "Name");
            return View();
        }

        // POST: Table2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Table1Id")] Table2 table2)
        {
            if (ModelState.IsValid)
            {
                _context.Add(table2);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Table1Id"] = new SelectList(_context.table1, "Id", "Name", table2.Table1Id);
            return View(table2);
        }

        // GET: Table2/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.table2 == null)
            {
                return NotFound();
            }

            var table2 = await _context.table2.FindAsync(id);
            if (table2 == null)
            {
                return NotFound();
            }
            ViewData["Table1Id"] = new SelectList(_context.table1, "Id", "Name", table2.Table1Id);
            return View(table2);
        }

        // POST: Table2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Table1Id")] Table2 table2)
        {
            if (id != table2.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(table2);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Table2Exists(table2.id))
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
            ViewData["Table1Id"] = new SelectList(_context.table1, "Id", "Name", table2.Table1Id);
            return View(table2);
        }

        // GET: Table2/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.table2 == null)
            {
                return NotFound();
            }

            var table2 = await _context.table2
                .Include(t => t.Table1)
                .FirstOrDefaultAsync(m => m.id == id);
            if (table2 == null)
            {
                return NotFound();
            }

            return View(table2);
        }

        // POST: Table2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.table2 == null)
            {
                return Problem("Entity set 'DataBaseContext.table2'  is null.");
            }
            var table2 = await _context.table2.FindAsync(id);
            if (table2 != null)
            {
                _context.table2.Remove(table2);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Table2Exists(int id)
        {
          return (_context.table2?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
