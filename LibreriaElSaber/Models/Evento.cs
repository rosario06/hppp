using System;
using System.ComponentModel.DataAnnotations;

namespace LibreriaElSaber.Models
{
    public class Evento
    {
        [Key]
        public int IdEvento { get; set; }
        public string NombreEvento { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaEvento { get; set; }
        public string Lugar { get; set; }

        public virtual ICollection<InscripcionEvento> InscripcionesEventos { get; set; }
    }
}
