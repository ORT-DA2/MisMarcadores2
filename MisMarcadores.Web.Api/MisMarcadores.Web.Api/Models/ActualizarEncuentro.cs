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
        public string NombreDeporte { get; set; }

        public Encuentro TransformarAEncuentro()
        {
            return new Encuentro
            {
                FechaHora = this.Fecha,
                Deporte = new Deporte { Nombre = this.NombreDeporte }
            };
        }
    }
}
