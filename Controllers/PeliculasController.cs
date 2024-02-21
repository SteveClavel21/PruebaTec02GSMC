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
    public class PeliculasController : Controller
    {
        private readonly PruebaTec02GSMCDBContext _context;

        public PeliculasController(PruebaTec02GSMCDBContext context)
        {
            _context = context;
        }

        // GET: Peliculas
        public async Task<IActionResult> Index()
        {
            var pruebaTec02GSMCDBContext = _context.Peliculas.Include(p => p.IdNavigation);
            return View(await pruebaTec02GSMCDBContext.ToListAsync());
        }

        // GET: Peliculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.IdNavigation)
                .FirstOrDefaultAsync(m => m.PeliculaId == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // GET: Peliculas/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Directores, "Id", "Nombre");
            return View();
        }

        // POST: Peliculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PeliculaId,Nombre,Descripcion,Id")] Pelicula pelicula, IFormFile imagen)
        {
            if (imagen !=null && imagen.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imagen.CopyToAsync(memoryStream);
                    pelicula.Imagen = memoryStream.ToArray();
                }
            }

            _context.Add(pelicula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         
        }

        // GET: Peliculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Directores, "Id", "Nombre", pelicula.Id);
            return View(pelicula);
        }

        // POST: Peliculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PeliculaId,Nombre,Descripcion,Id")] Pelicula pelicula, IFormFile imagen)
        {
            if (id != pelicula.PeliculaId)
            {
                return NotFound();
            }

            if (imagen != null && imagen.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imagen.CopyToAsync(memoryStream);
                    pelicula.Imagen = memoryStream.ToArray();
                }
                _context.Update(pelicula);
                await _context.SaveChangesAsync();
            }
            else
            {
                var producFind = await _context.Peliculas.FirstOrDefaultAsync(s => s.PeliculaId == pelicula.PeliculaId);
                if (producFind?.Imagen?.Length > 0)
                    pelicula.Imagen = producFind.Imagen;
                producFind.Nombre = pelicula.Nombre;
                producFind.Descripcion = pelicula.Descripcion;

                producFind.Id = pelicula.Id;
                _context.Update(producFind);
                await _context.SaveChangesAsync();
            }
           

                   
                   try { 
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PeliculaExists(id))
                        {
                            return NotFound();
                        }
                    }
                
                return RedirectToAction(nameof(Index));
            
            //ViewData["Id"] = new SelectList(_context.Directores, "Id", "Id", pelicula.Id);
            //return View(pelicula);
        }

        // GET: Peliculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.IdNavigation)
                .FirstOrDefaultAsync(m => m.PeliculaId == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Peliculas == null)
            {
                return Problem("Entity set 'PruebaTec02GSMCDBContext.Peliculas'  is null.");
            }
            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula != null)
            {
                _context.Peliculas.Remove(pelicula);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(int id)
        {
          return (_context.Peliculas?.Any(e => e.PeliculaId == id)).GetValueOrDefault();
        }
    }
}
