using MisMarcadores.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MisMarcadores.Web.Api.Models
{
    public class AgregarUsuario
    {
        [Required]
        public string Mail { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public string NombreUsuario { get; set; }

        [Required]
        public string Contraseña { get; set; }

        [Required]
        public bool Administrador { get; set; }

        public Usuario TransformarAUsuario()
        {
            return new Usuario
            {
                Nombre = this.Nombre,
                Apellido = this.Apellido,
                Mail = this.Mail,
                NombreUsuario = this.NombreUsuario,
                Contraseña = this.Contraseña,
                Administrador = this.Administrador
                
            };
        }

    }
}
