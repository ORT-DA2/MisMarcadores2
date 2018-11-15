using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisMarcadores.Web.Api.Models
{
    public class MostrarParticipante
    {
        public Guid Id { get; set; }
        public String Nombre { get; set; }
        public String Foto { get; set; }
        public string Deporte { get; set; }
        public bool EsEquipo { get; set; }


        public MostrarParticipante(Participante participante)
        {
            Id = participante.Id;
            Nombre = participante.Nombre;
            Foto = participante.Foto;
            Deporte = participante.Deporte.Nombre;
            EsEquipo = participante.EsEquipo;
        }
    }
}

