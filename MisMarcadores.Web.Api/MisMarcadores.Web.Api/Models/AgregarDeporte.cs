using MisMarcadores.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MisMarcadores.Web.Api.Models
{
    public class AgregarDeporte
    {
        [Required]
        public string Nombre { get; set; }

        public Deporte TransformarADeporte()
        {
            return new Deporte
            {
                Nombre = this.Nombre
            };
        }
    }
}
