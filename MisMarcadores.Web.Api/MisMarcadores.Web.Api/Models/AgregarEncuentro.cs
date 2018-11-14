using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MisMarcadores.Web.Api.Models
{
    public class AgregarEncuentro
    {
        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public string NombreDeporte { get; set; }

        [Required]
        public ICollection<ParticipanteEncuentro> ParticipanteEncuentro { get; set; }

        public Encuentro TransformarAEncuentro()
        {
            return new Encuentro
            {
                FechaHora = this.Fecha,
                ParticipanteEncuentro = this.ParticipanteEncuentro,
                Deporte = new Deporte { Nombre = this.NombreDeporte },
                
                
            };
        }
    }
}
