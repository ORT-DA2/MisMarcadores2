using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisMarcadores.Web.Api.Models
{
    public class MostrarPuntaje
    {
        public int Puntos { get; set; }
        public string Participante { get; set; }

        public MostrarPuntaje(ParticipanteEncuentro puntaje)
        {
            Puntos = puntaje.PuntosObtenidos;
            Participante = puntaje.Participante.Nombre;           
        }
    }
}
