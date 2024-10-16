using LibreriaElSaber.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace LibreriaElSaber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscripcionEventoController : ControllerBase
    {
        private readonly List<InscripcionEvento> _inscripciones;

        public InscripcionEventoController()
        {
            _inscripciones = new List<InscripcionEvento>
            {
                new InscripcionEvento { IdInscripcion = 1, IdUsuario = 1, IdEvento = 1 },
                new InscripcionEvento { IdInscripcion = 2, IdUsuario = 2, IdEvento = 1 },
                new InscripcionEvento { IdInscripcion = 3, IdUsuario = 3, IdEvento = 2 }
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<InscripcionEvento>> GetInscripciones()
        {
            return _inscripciones;
        }

        [HttpGet("{id}")]
        public ActionResult<InscripcionEvento> GetInscripcion(int id)
        {
            var inscripcion = _inscripciones.Find(i => i.IdInscripcion == id);
            if (inscripcion == null)
            {
                return NotFound();
            }
            return inscripcion;
        }

        [HttpPost]
        public ActionResult<InscripcionEvento> CreateInscripcion(InscripcionEvento inscripcion)
        {
            inscripcion.IdInscripcion = _inscripciones.Count + 1;
            _inscripciones.Add(inscripcion);
            return CreatedAtAction(nameof(GetInscripcion), new { id = inscripcion.IdInscripcion }, inscripcion);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateInscripcion(int id, InscripcionEvento updatedInscripcion)
        {
            var inscripcion = _inscripciones.Find(i => i.IdInscripcion == id);
            if (inscripcion == null)
            {
                return NotFound();
            }
            inscripcion.IdUsuario = updatedInscripcion.IdUsuario;
            inscripcion.IdEvento = updatedInscripcion.IdEvento;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInscripcion(int id)
        {
            var inscripcion = _inscripciones.Find(i => i.IdInscripcion == id);
            if (inscripcion == null)
            {
                return NotFound();
            }
            _inscripciones.Remove(inscripcion);
            return NoContent();
        }
    }
}
