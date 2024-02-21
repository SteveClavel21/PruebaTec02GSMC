using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaTec02GSMC.Models;

namespace PruebaTec02GSMC.Controllers
{
    public class DirectoresController : Controller
    {
        private readonly PruebaTec02GSMCDBContext _context;

        public DirectoresController(PruebaTec02GSMCDBContext context)
        {
            _context = context;
        }

        // GET: Directores
        public async Task<IActionResult> Index()
        {
              return _context.Directores != null ? 
                          View(await _context.Directores.ToListAsync()) :
                          Problem("Entity set 'PruebaTec02GSMCDBContext.Directores'  is null.");
        }

        // GET: Directores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Directores == null)
            {
                return NotFound();
            }

            var directore = await _context.Directores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (directore == null)
            {
                return NotFound();
            }

            return View(directore);
        }

        // GET: Directores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Directores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Directore directore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(directore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(directore);
        }

        // GET: Directores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Directores == null)
            {
                return NotFound();
            }

            var directore = await _context.Directores.FindAsync(id);
            if (directore == null)
            {
                return NotFound();
            }
            return View(directore);
        }

        // POST: Directores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Directore directore)
        {
            if (id != directore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(directore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectoreExists(directore.Id))
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
            return View(directore);
        }

        // GET: Directores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Directores == null)
            {
                return NotFound();
            }

            var directore = await _context.Directores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (directore == null)
            {
                return NotFound();
            }

            return View(directore);
        }

        // POST: Directores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Directores == null)
            {
                return Problem("Entity set 'PruebaTec02GSMCDBContext.Directores'  is null.");
            }
            var directore = await _context.Directores.FindAsync(id);
            if (directore != null)
            {
                _context.Directores.Remove(directore);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DirectoreExists(int id)
        {
          return (_context.Directores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
