using LibreriaElSaber.Data;
using LibreriaElSaber.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibreriaElSaber.Controllers
{
    public class UsersController : Controller
    {
        private readonly LibreriaElSaberContext _context;
        private readonly ILogger<UsersController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsersController(LibreriaElSaberContext context, ILogger<UsersController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user, IFormFile Imagen)
        {
            if (!string.IsNullOrWhiteSpace(user.Nombre))  // Verifica que el nombre no esté vacío
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
                    user.Imagen = imageUrl; // Guardar la URL en el modelo
                }

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Usuarios.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user, IFormFile Imagen)
        {
            if (id != user.IdUsuario)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(user.Nombre))
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
                        user.Imagen = imageUrl; // Guardar la URL en la propiedad del modelo
                    }
                    else
                    {
                        // Si no se subió una nueva imagen, conservar la imagen existente
                        var existingUser = await _context.Usuarios.FindAsync(id);
                        if (existingUser != null)
                        {
                            user.Imagen = existingUser.Imagen; // Conservar la URL existente
                        }
                    }

                    // Actualizar el usuario
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.IdUsuario))
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

            return View(user);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Usuarios.FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (!string.IsNullOrEmpty(user.Imagen))
            {
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, user.Imagen.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Usuarios.FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
