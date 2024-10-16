using LibreriaElSaber.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LibreriaElSaber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly List<Prestamo> _prestamos;

        public PrestamoController()
        {
            _prestamos = new List<Prestamo>
            {
                new Prestamo { IdPrestamo = 1, IdLibro = 1, IdUsuario = 1, FechaPrestamo = DateTime.Now, FechaDevolucion = DateTime.Now.AddDays(7), Devuelto = false },
                new Prestamo { IdPrestamo = 2, IdLibro = 2, IdUsuario = 2, FechaPrestamo = DateTime.Now, FechaDevolucion = DateTime.Now.AddDays(14), Devuelto = false },
                new Prestamo { IdPrestamo = 3, IdLibro = 3, IdUsuario = 3, FechaPrestamo = DateTime.Now, FechaDevolucion = DateTime.Now.AddDays(21), Devuelto = true }
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<Prestamo>> GetPrestamos()
        {
            return _prestamos;
        }

        [HttpGet("{id}")]
        public ActionResult<Prestamo> GetPrestamo(int id)
        {
            var prestamo = _prestamos.Find(p => p.IdPrestamo == id);
            if (prestamo == null)
            {
                return NotFound();
            }
            return prestamo;
        }

        [HttpPost]
        public ActionResult<Prestamo> CreatePrestamo(Prestamo prestamo)
        {
            prestamo.IdPrestamo = _prestamos.Count + 1;
            _prestamos.Add(prestamo);
            return CreatedAtAction(nameof(GetPrestamo), new { id = prestamo.IdPrestamo }, prestamo);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePrestamo(int id, Prestamo updatedPrestamo)
        {
            var prestamo = _prestamos.Find(p => p.IdPrestamo == id);
            if (prestamo == null)
            {
                return NotFound();
            }
            prestamo.IdLibro = updatedPrestamo.IdLibro;
            prestamo.IdUsuario = updatedPrestamo.IdUsuario;
            prestamo.FechaPrestamo = updatedPrestamo.FechaPrestamo;
            prestamo.FechaDevolucion = updatedPrestamo.FechaDevolucion;
            prestamo.Devuelto = updatedPrestamo.Devuelto;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePrestamo(int id)
        {
            var prestamo = _prestamos.Find(p => p.IdPrestamo == id);
            if (prestamo == null)
            {
                return NotFound();
            }
            _prestamos.Remove(prestamo);
            return NoContent();
        }
    }
}
