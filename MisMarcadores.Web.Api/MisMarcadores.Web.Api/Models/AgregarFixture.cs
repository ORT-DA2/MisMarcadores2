using MisMarcadores.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace MisMarcadores.Web.Api.Models
{
    public class AgregarFixture
    {
        [Required]
        public string Tipo { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public string Deporte { get; set; }

    }
}
