using Microsoft.EntityFrameworkCore;
using MisMarcadores.Data.Entities;

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
    }

}
