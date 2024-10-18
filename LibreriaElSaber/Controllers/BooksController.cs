using LibreriaElSaber.Data;
using LibreriaElSaber.Models; // Ajusta según tu espacio de nombres
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibreriaElSaber.Controllers
{

    public class BooksController : Controller
    {
        private readonly LibreriaElSaberContext _context;
        private readonly ILogger<BooksController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksController(LibreriaElSaberContext context, ILogger<BooksController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Libros.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book, IFormFile Imagen)
        {
            if (!string.IsNullOrWhiteSpace(book.Titulo))  // Verifica que el título no esté vacío
            {
                if (Imagen != null && Imagen.Length > 0)
                {
                    // Establecer la ruta del archivo en el servidor
                    string imageFileName = Guid.NewGuid().ToString() + Path.GetExtension(Imagen.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", imageFileName);

                    // Asegurarse de que la carpeta existe
                    if (!Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, "images")))
                    {
                        Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, "images"));
                    }

                    // Guardar la imagen
                    using (var fileStream = new FileStream(imagePath, FileMode.Create))
                    {
                        await Imagen.CopyToAsync(fileStream);
                    }

                    // Construir la URL de la imagen
                    string imageUrl = $"{Request.Scheme}://{Request.Host}/images/{imageFileName}";
                    book.Imagen = imageUrl; // Guardar la URL en el modelo
                }

                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Libros.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book, IFormFile Imagen)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(book.Titulo))
            {
                try
                {
                    // Si se proporciona una nueva imagen
                    if (Imagen != null && Imagen.Length > 0)
                    {
                        // Generar el nombre único de archivo y la ruta completa
                        string imageFileName = Guid.NewGuid().ToString() + Path.GetExtension(Imagen.FileName);
                        string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", imageFileName);

                        // Asegurarse de que la carpeta exista
                        if (!Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, "images")))
                        {
                            Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, "images"));
                        }

                        // Guardar la nueva imagen
                        using (var fileStream = new FileStream(imagePath, FileMode.Create))
                        {
                            await Imagen.CopyToAsync(fileStream);
                        }

                        // Construir la URL de la imagen
                        string imageUrl = $"{Request.Scheme}://{Request.Host}/images/{imageFileName}";
                        book.Imagen = imageUrl; // Guardar la URL en la propiedad del modelo
                    }
                    else
                    {
                        // Si no se subió una nueva imagen, conservar la imagen existente
                        var existingBook = await _context.Libros.FindAsync(id);
                        if (existingBook != null)
                        {
                            book.Imagen = existingBook.Imagen; // Conservar la URL existente
                        }
                    }

                    // Actualizar el libro
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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

            return View(book);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Libros.FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Libros.FindAsync(id);
            if (!string.IsNullOrEmpty(book.Imagen))
            {
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, book.Imagen.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _context.Libros.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Libros.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Libros.FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // Método para ver todos los libros en formato grid
        public async Task<IActionResult> Grid()
        {
            var libros = await _context.Libros.ToListAsync();
            return View(libros);
        }
    }
}
