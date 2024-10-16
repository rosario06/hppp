namespace LibreriaElSaber.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string ISBN { get; set; }
        public string Genero { get; set; }
        public string Editorial { get; set; }
        public int AnioPublicacion { get; set; }
        public int CantidadDisponible { get; set; }
        public int CantidadTotal { get; set; }

        public virtual ICollection<Prestamo> Prestamos { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }
    }
}
