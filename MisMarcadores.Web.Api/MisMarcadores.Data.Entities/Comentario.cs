using System;

namespace MisMarcadores.Data.Entities
{
    public class Comentario
    {
        public Guid Id { get; set; }
        public Usuario Usuario { get; set; }
        public Encuentro Encuentro { get; set; }
        public String Texto { get; set; }

        public Comentario() {
            Usuario = new Usuario();
            Encuentro = new Encuentro();
        }
    }
}
