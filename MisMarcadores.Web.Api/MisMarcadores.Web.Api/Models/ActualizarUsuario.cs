using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MisMarcadores.Web.Api.Models
{
    public class ActualizarUsuario
    {
        [Required]
        public string Mail { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string NombreUsuario { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public string Contraseña { get; set; }

        [Required]
        public bool Administrador { get; set; }

        public Usuario TransformarAUsuario()
        {
            return new Usuario
            {
                NombreUsuario = this.NombreUsuario,
                Nombre = this.Nombre,
                Apellido = this.Apellido,
                Mail = this.Mail,
                Contraseña = this.Contraseña,
                Administrador = this.Administrador
            };
        }

    }
}
