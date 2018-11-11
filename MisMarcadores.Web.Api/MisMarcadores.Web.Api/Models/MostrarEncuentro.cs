
using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MisMarcadores.Web.Api.Models
{
    public class MostrarEncuentro
    {
      
        public DateTime Fecha { get; set; }
        public string NombreDeporte { get; set; }
        public List<MostrarPuntaje> Puntajes { get; set; }

        public MostrarEncuentro(Encuentro encuentro)
        {
            Fecha = encuentro.FechaHora;
            NombreDeporte = encuentro.Deporte.Nombre;
            Puntajes = new List<MostrarPuntaje>();
            foreach (Puntaje item in encuentro.Puntaje)
            {
                MostrarPuntaje p = new MostrarPuntaje(item);
                Puntajes.Add(p);
            }
            
        }
    }
}