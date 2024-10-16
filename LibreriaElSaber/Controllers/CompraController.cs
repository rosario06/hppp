using LibreriaElSaber.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace LibreriaElSaber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly List<Compra> _compras;

        public CompraController()
        {
            _compras = new List<Compra>
            {
                new Compra { IdCompra = 1, IdLibro = 1, IdUsuario = 1, FechaCompra = DateTime.Now, Precio = 9.99m },
                new Compra { IdCompra = 2, IdLibro = 2, IdUsuario = 2, FechaCompra = DateTime.Now, Precio = 14.99m },
                new Compra { IdCompra = 3, IdLibro = 3, IdUsuario = 3, FechaCompra = DateTime.Now, Precio = 19.99m }
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<Compra>> GetCompras()
        {
            return _compras;
        }

        [HttpGet("{id}")]
        public ActionResult<Compra> GetCompra(int id)
        {
            var compra = _compras.Find(c => c.IdCompra == id);
            if (compra == null)
            {
                return NotFound();
            }
            return compra;
        }

        [HttpPost]
        public ActionResult<Compra> CreateCompra(Compra compra)
        {
            compra.IdCompra = _compras.Count + 1;
            _compras.Add(compra);
            return CreatedAtAction(nameof(GetCompra), new { id = compra.IdCompra }, compra);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCompra(int id, Compra updatedCompra)
        {
            var compra = _compras.Find(c => c.IdCompra == id);
            if (compra == null)
            {
                return NotFound();
            }
            compra.IdLibro = updatedCompra.IdLibro;
            compra.IdUsuario = updatedCompra.IdUsuario;
            compra.FechaCompra = updatedCompra.FechaCompra;
            compra.Precio = updatedCompra.Precio;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCompra(int id)
        {
            var compra = _compras.Find(c => c.IdCompra == id);
            if (compra == null)
            {
                return NotFound();
            }
            _compras.Remove(compra);
            return NoContent();
        }
    }
}
