using LibreriaElSaber.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaElSaber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly List<Evento> _eventos;

        public EventoController()
        {
            _eventos = new List<Evento>
            {
                new Evento { IdEvento = 1, NombreEvento = "Evento 1", Descripcion = "Descripción del Evento 1", FechaEvento = DateTime.Now, Lugar = "Lugar del Evento 1" },
                new Evento { IdEvento = 2, NombreEvento = "Evento 2", Descripcion = "Descripción del Evento 2", FechaEvento = DateTime.Now, Lugar = "Lugar del Evento 2" },
                new Evento { IdEvento = 3, NombreEvento = "Evento 3", Descripcion = "Descripción del Evento 3", FechaEvento = DateTime.Now, Lugar = "Lugar del Evento 3" }
            };
        }

        [HttpGet]
        public ActionResult<IEnumerable<Evento>> GetEventos()
        {
            return _eventos;
        }

        [HttpGet("{id}")]
        public ActionResult<Evento> GetEvento(int id)
        {
            var evento = _eventos.Find(e => e.IdEvento == id);
            if (evento == null)
            {
                return NotFound();
            }
            return evento;
        }

        [HttpPost]
        public ActionResult<Evento> CreateEvento(Evento evento)
        {
            evento.IdEvento = _eventos.Count + 1;
            _eventos.Add(evento);
            return CreatedAtAction(nameof(GetEvento), new { id = evento.IdEvento }, evento);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEvento(int id, Evento updatedEvento)
        {
            var evento = _eventos.Find(e => e.IdEvento == id);
            if (evento == null)
            {
                return NotFound();
            }
            evento.NombreEvento = updatedEvento.NombreEvento;
            evento.Descripcion = updatedEvento.Descripcion;
            evento.FechaEvento = updatedEvento.FechaEvento;
            evento.Lugar = updatedEvento.Lugar;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvento(int id)
        {
            var evento = _eventos.Find(e => e.IdEvento == id);
            if (evento == null)
            {
                return NotFound();
            }
            _eventos.Remove(evento);
            return NoContent();
        }
    }
}
