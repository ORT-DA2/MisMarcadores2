using MisMarcadores.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MisMarcadores.Web.Api.Models
{
    public class ActualizarDeporte
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public bool EsIndividual { get; set; }

        public Deporte TransformarADeporte()
        {
            return new Deporte
            {
                Nombre = this.Nombre,
                EsIndividual = this.EsIndividual
            };
        }
    }
}
