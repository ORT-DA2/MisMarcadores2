using Microsoft.EntityFrameworkCore;
using MisMarcadores.Data.Entities;

namespace MisMarcadores.Data.DataAccess
{
    public class MisMarcadoresContext : DbContext
    {
        public MisMarcadoresContext(DbContextOptions<MisMarcadoresContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Deporte> Deportes { get; set; }
        public DbSet<Encuentro> Encuentros { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }
        public DbSet<Sesion> Sesiones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Deporte>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Participante>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Encuentro>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Comentario>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Favorito>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Sesion>().HasKey(s => s.NombreUsuario);
        }

    }
}