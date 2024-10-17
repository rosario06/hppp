using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using LibreriaElSaber.Models; // Ajusta según tu espacio de nombres
using System;
using System.Linq;
using System.Threading.Tasks;
using LibreriaElSaber.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibreriaElSaber.Controllers
{
    public class CompraController : Controller
    {
        private readonly LibreriaElSaberContext _context;
        private readonly ILogger<CompraController> _logger;

        public CompraController(LibreriaElSaberContext context, ILogger<CompraController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var compras = await _context.Compras
                            .Include(c => c.Libro) // Incluye información sobre el libro
                            .Include(c => c.Usuario) // Incluye información sobre el usuario
                            .ToListAsync();

            return View(compras);
        }

        public IActionResult Create()
        {
            ViewBag.ListaLibros = new SelectList(_context.Libros, "Id", "Titulo"); // Asegúrate de tener la lista de libros disponible
            ViewBag.ListaUsuarios = new SelectList(_context.Usuarios, "IdUsuario", "Nombre"); // Asegúrate de tener la lista de usuarios disponible

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Compra compra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ListaLibros = new SelectList(_context.Libros, "Id", "Titulo", compra.IdLibro);
            ViewBag.ListaUsuarios = new SelectList(_context.Usuarios, "IdUsuario", "Nombre", compra.IdUsuario);

            return View(compra);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }

            ViewBag.ListaLibros = new SelectList(_context.Libros, "Id", "Titulo", compra.IdLibro);
            ViewBag.ListaUsuarios = new SelectList(_context.Usuarios, "IdUsuario", "Nombre", compra.IdUsuario);

            return View(compra);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Compra compra)
        {
            if (id != compra.IdCompra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraExists(compra.IdCompra))
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

            ViewBag.ListaLibros = new SelectList(_context.Libros, "Id", "Titulo", compra.IdLibro);
            ViewBag.ListaUsuarios = new SelectList(_context.Usuarios, "IdUsuario", "Nombre", compra.IdUsuario);

            return View(compra);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                            .Include(c => c.Libro) // Incluye información sobre el libro
                            .Include(c => c.Usuario) // Incluye información sobre el usuario
                            .FirstOrDefaultAsync(m => m.IdCompra == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compra = await _context.Compras.FindAsync(id);
            _context.Compras.Remove(compra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraExists(int id)
        {
            return _context.Compras.Any(e => e.IdCompra == id);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var compra = await _context.Compras
                            .Include(c => c.Libro) // Incluye información sobre el libro
                            .Include(c => c.Usuario) // Incluye información sobre el usuario
                            .FirstOrDefaultAsync(m => m.IdCompra == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }
    }
}
