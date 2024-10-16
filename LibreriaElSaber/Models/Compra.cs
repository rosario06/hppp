using System;
using System.ComponentModel.DataAnnotations;

namespace LibreriaElSaber.Models
{
    public class Compra
    {
        [Key]
        public int IdCompra { get; set; }
        public int IdLibro { get; set; } // Foreign key to the Libros table
        public int IdUsuario { get; set; } // Foreign key to the Usuarios table
        public DateTime FechaCompra { get; set; }
        public decimal Precio { get; set; }

        // Navigation property to the Libro entity
        public virtual Book Libro { get; set; }  // This is crucial!

        // Navigation property to the Usuario entity
        public virtual User Usuario { get; set; }
    }

}
