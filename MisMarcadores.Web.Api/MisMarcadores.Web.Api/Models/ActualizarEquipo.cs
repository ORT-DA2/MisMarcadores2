using MisMarcadores.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MisMarcadores.Web.Api.Models
{
    public class ActualizarEquipo
    {
        [Required]
        public string Nombre { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Foto { get; set; }

        [Required]
        public string NombreDeporte { get; set; }

        public Equipo TransformarAEquipo()
        {
            return new Equipo
            {
                Nombre = this.Nombre,
                Foto = this.Foto,
                Deporte = new Deporte { Nombre = this.NombreDeporte }
            };
        }
    }
}
