using System.ComponentModel.DataAnnotations;

namespace MisMarcadores.Web.Api.Models
{
    public class Login
    {
        [Required]
        public string NombreUsuario { get; set; }

        [Required]
        public string Contrasena { get; set; }

    }
}
