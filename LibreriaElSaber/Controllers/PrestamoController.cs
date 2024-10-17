using LibreriaElSaber.Data;
using LibreriaElSaber.Models; // Ajusta según tu espacio de nombres
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibreriaElSaber.Controllers
{
    public class PrestamoController : Controller
    {
        private readonly LibreriaElSaberContext _context;
        private readonly ILogger<PrestamoController> _logger;

        public PrestamoController(LibreriaElSaberContext context, ILogger<PrestamoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Prestamos.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.ListaLibros = new SelectList(_context.Libros, "Id", "Titulo"); // Asegúrate de tener la lista de libros disponible
            ViewBag.ListaUsuarios = new SelectList(_context.Usuarios, "IdUsuario", "Nombre"); // Asegúrate de tener la lista de usuarios disponible

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                prestamo.FechaPrestamo = DateTime.Now;
                prestamo.FechaDevolucion = DateTime.Now.AddDays(7); // Devolución en 7 días
                prestamo.Devuelto = false; // Por defecto, no devuelto

                _context.Add(prestamo);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.ListaLibros = new SelectList(_context.Libros, "Id", "Titulo", prestamo.IdLibro);
            ViewBag.ListaUsuarios = new SelectList(_context.Usuarios, "IdUsuario", "Nombre", prestamo.IdUsuario);

            return View(prestamo);
        }

        public async Task<IActionResult> Devolver(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos.FindAsync(id);

            if (prestamo == null)
            {
                return NotFound();
            }

            prestamo.Devuelto = true;
            prestamo.FechaDevolucion = DateTime.Now;

            _context.Update(prestamo);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }

            ViewBag.ListaLibros = new SelectList(_context.Libros, "Id", "Titulo", prestamo.IdLibro);
            ViewBag.ListaUsuarios = new SelectList(_context.Usuarios, "IdUsuario", "Nombre", prestamo.IdUsuario);

            return View(prestamo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Prestamo prestamo)
        {
            if (id != prestamo.IdPrestamo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prestamo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestamoExists(prestamo.IdPrestamo))
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

            ViewBag.ListaLibros = new SelectList(_context.Libros, "Id", "Titulo", prestamo.IdLibro);
            ViewBag.ListaUsuarios = new SelectList(_context.Usuarios, "IdUsuario", "Nombre", prestamo.IdUsuario);

            return View(prestamo);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                            .Include(p => p.Libro) // Incluye información sobre el libro
                            .Include(p => p.Usuario) // Incluye información sobre el usuario
                            .FirstOrDefaultAsync(m => m.IdPrestamo == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);
            _context.Prestamos.Remove(prestamo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrestamoExists(int id)
        {
            return _context.Prestamos.Any(e => e.IdPrestamo == id);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                            .Include(p => p.Libro) // Incluye información sobre el libro
                            .Include(p => p.Usuario) // Incluye información sobre el usuario
                            .FirstOrDefaultAsync(m => m.IdPrestamo == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }
    }
}
