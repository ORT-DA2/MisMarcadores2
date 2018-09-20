using System;
using System.Collections.Generic;

namespace MisMarcadores.Data.Entities
{
    public class Encuentro
    {
        public Guid Id { get; set; }
        public DateTime FechaHora { get; set; }
        public Deporte Deporte { get; set; }
        public Equipo EquipoLocal { get; set; }
        public Equipo EquipoVisitante { get; set; }
        public List<Comentario> Comentarios { get; set; }

        public Encuentro() {
            FechaHora = new DateTime();
            Deporte = new Deporte();
            EquipoLocal = new Equipo();
            EquipoVisitante = new Equipo();
            Comentarios = new List<Comentario>();
        }
    }
}
