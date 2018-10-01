using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MisMarcadores.Data.Entities;
using System.IO;

namespace MisMarcadores.Web.Api.Models
{
    public class MisMarcadoresContext : DbContext
    {
        public MisMarcadoresContext(DbContextOptions<MisMarcadoresContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Deporte> Deportes { get; set; }
        public DbSet<Encuentro> Encuentros { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Sesion> Sesiones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Sesion>().HasKey(s => s.NombreUsuario);
        }

    }
}