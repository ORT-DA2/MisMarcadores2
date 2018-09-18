using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MisMarcadores.Data.Entities
{
    [Table("Encuentros")]
    public class Encuentro
    {
        [Key]
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
