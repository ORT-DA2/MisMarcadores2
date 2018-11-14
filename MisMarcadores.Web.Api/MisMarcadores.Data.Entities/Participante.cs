using System;
using System.Collections.Generic;

namespace MisMarcadores.Data.Entities
{
    public class Participante
    {
        public Guid Id { get; set; }
        public String Nombre { get; set; }
        public String Foto { get; set; }
        public Deporte Deporte { get; set; }
        public bool EsEquipo  { get; set; }
        public virtual ICollection<ParticipanteEncuentro> ParticipanteEncuentro { get; set; }

        public Participante() {
            Deporte = new Deporte();
        }

        public override bool Equals(object obj)
        {
            bool igual = false;
            if (obj is Participante)
            {
                igual = Nombre.Equals(((Participante)obj).Nombre);
            }
            return igual;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}