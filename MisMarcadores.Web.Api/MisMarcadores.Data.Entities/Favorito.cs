using System;

namespace MisMarcadores.Data.Entities
{
    public class Favorito
    {
        public Guid Id { get; set; }
        public String NombreUsuario { get; set; }
        public Guid IdParticipante { get; set; }

        public Favorito()
        {

        }
    }
}

