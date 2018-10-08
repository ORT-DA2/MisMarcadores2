using System;

namespace MisMarcadores.Data.Entities
{
    public class Comentario
    {
        public Guid Id { get; set; }
        public String NombreUsuario { get; set; }
        public Guid IdEncuentro { get; set; }
        public String Texto { get; set; }

        public Comentario() {
           
        }
    }
}
