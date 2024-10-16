using System.ComponentModel.DataAnnotations;

namespace LibreriaElSaber.Models
{
    public class User
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contrasena { get; set; }
        public string TipoUsuario { get; set; }

        public virtual ICollection<InscripcionEvento> InscripcionesEventos { get; set; }
        public virtual ICollection<Prestamo> Prestamos { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }
    }
}
