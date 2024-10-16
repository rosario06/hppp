using Microsoft.EntityFrameworkCore;
using LibreriaElSaber.Models;

namespace LibreriaElSaber.Data
{
    using Microsoft.EntityFrameworkCore;
    // ... other using statements ...

    public class LibreriaElSaberContext : DbContext
    {
        public LibreriaElSaberContext(DbContextOptions<LibreriaElSaberContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Libros { get; set; }
        public DbSet<User> Usuarios { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<InscripcionEvento> InscripcionesEventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships between entities using Fluent API

            // Prestamos: One-to-many with Libros and Usuarios
            modelBuilder.Entity<Prestamo>()
                .HasOne(p => p.Libro) // Assuming Prestamo has a Libro property (navigation property)
                .WithMany(l => l.Prestamos) // Assuming Libro has a collection of Prestamos
                .HasForeignKey(p => p.IdLibro); // Assuming IdLibro is the foreign key in Prestamo

            modelBuilder.Entity<Prestamo>()
                .HasOne(p => p.Usuario) // Assuming Prestamo has a Usuario property
                .WithMany(u => u.Prestamos) // Assuming Usuario has a collection of Prestamos
                .HasForeignKey(p => p.IdUsuario); // Assuming IdUsuario is the foreign key


            // Compras: One-to-many with Libros and Usuarios  (Similar configuration)
            modelBuilder.Entity<Compra>()
                .HasOne(c => c.Libro)
                .WithMany(l => l.Compras)
                .HasForeignKey(c => c.IdLibro);

            modelBuilder.Entity<Compra>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Compras)
                .HasForeignKey(c => c.IdUsuario);

            // InscripcionesEventos: Many-to-many with Usuarios and Eventos

            modelBuilder.Entity<InscripcionEvento>()
                .HasKey(ie => new { ie.IdUsuario, ie.IdEvento }); //Composite key for many-to-many

            modelBuilder.Entity<InscripcionEvento>()
                .HasOne(ie => ie.Usuario)
                .WithMany(u => u.InscripcionesEventos)
                .HasForeignKey(ie => ie.IdUsuario);

            modelBuilder.Entity<InscripcionEvento>()
                .HasOne(ie => ie.Evento)
                .WithMany(e => e.InscripcionesEventos)
                .HasForeignKey(ie => ie.IdEvento);


            // Add similar configurations for any other relationships you have
            base.OnModelCreating(modelBuilder);
        }
    }

}
