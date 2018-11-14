using System;
using System.Collections.Generic;
using System.Text;

namespace MisMarcadores.Data.Entities
{
    public class ParticipanteEncuentro
    {
        public Guid ParticipanteId { get; set; }
        public virtual Participante Participante { get; set; }
        public int PuntosObtenidos { get; set; }
        public Guid EncuentroId  { get; set; }
        public virtual Encuentro Encuentro { get; set; }

        public ParticipanteEncuentro(){

        }
    }
}
