using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibreriaElSaber.Models
{
    public class InscripcionEvento
    {
        public int IdInscripcion { get; set; }
        [Key]
        [Column(Order = 1)] // Order is needed for composite key
        public int IdUsuario { get; set; }
        [Key]
        [Column(Order = 2)] // Order is needed for composite key
        public int IdEvento { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual User Usuario { get; set; }

        [ForeignKey("IdEvento")]
        public virtual Evento Evento { get; set; }
    }
}
