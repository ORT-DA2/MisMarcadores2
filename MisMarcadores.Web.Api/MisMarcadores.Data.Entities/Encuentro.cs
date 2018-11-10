using System;
using System.Collections.Generic;

namespace MisMarcadores.Data.Entities
{
    public class Encuentro
    {
        public Guid Id { get; set; }
        public DateTime FechaHora { get; set; }
        public Deporte Deporte { get; set; }
        public Participante ParticipanteLocal { get; set; }
        public Participante ParticipanteVisitante { get; set; }

        public Encuentro()
        {
            FechaHora = new DateTime();
            Deporte = new Deporte();
            ParticipanteLocal = new Participante();
            ParticipanteVisitante = new Participante();
        }
    }
}
