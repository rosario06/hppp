using System;
using System.ComponentModel.DataAnnotations;

namespace LibreriaElSaber.Models
{
    public class Prestamo
    {
        [Key]
        public int IdPrestamo { get; set; }
        public int IdLibro { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaDevolucion { get; set; }
        public bool Devuelto { get; set; }

        public virtual Book Libro { get; set; } // Crucial navigation property
        public virtual User Usuario { get; set; }
    } 
}
