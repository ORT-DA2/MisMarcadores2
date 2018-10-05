using MisMarcadores.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace MisMarcadores.Web.Api.Models
{
    public class ActualizarEncuentro
    {
        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public string EquipoLocal { get; set; }

        [Required]
        public string EquipoVisitante { get; set; }

        [Required]
        public string NombreDeporte { get; set; }

        public Encuentro TransformarAEncuentro()
        {
            return new Encuentro
            {
                FechaHora = this.Fecha,
                EquipoLocal = new Equipo { Nombre = this.EquipoLocal },
                EquipoVisitante = new Equipo { Nombre = this.EquipoVisitante },
                Deporte = new Deporte { Nombre = this.NombreDeporte }
            };
        }
    }
}
