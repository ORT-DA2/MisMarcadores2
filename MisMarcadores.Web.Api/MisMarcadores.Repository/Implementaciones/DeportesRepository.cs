﻿using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisMarcadores.Repository
{
    public class DeportesRepository : GenericRepository<Deporte>, IDeportesRepository
    {
        public DeportesRepository(MisMarcadoresContext context) : base(context) { }

        public void BorrarDeporte(string nombre)
        {
            Deporte deporte = context.Deportes.FirstOrDefault(d => d.Nombre == nombre);
            var encuentros = context.Encuentros.Where(e => e.Deporte.Nombre.Equals(deporte.Nombre));
            foreach (var encuentro in encuentros)
            {
                context.Encuentros.Remove(encuentro);
            }
            var participantes = context.Participantes.Where(e => e.Deporte.Nombre.Equals(deporte.Nombre));
            foreach (var participante in participantes)
            {
                context.Participantes.Remove(participante);
            }
            context.Deportes.Remove(deporte);
        }

        public void ModificarDeporte(Deporte deporte)
        {
            var deporteOriginal = GetByID(deporte.Id);
            context.Deportes.Attach(deporteOriginal);
            var entry = context.Entry(deporteOriginal);
            entry.CurrentValues.SetValues(deporte);
        }

        public Deporte ObtenerDeportePorNombre(string nombre)
        {
            return context.Deportes.FirstOrDefault(x => x.Nombre.Equals(nombre));
        }
    }
}
