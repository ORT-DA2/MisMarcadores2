using MisMarcadores.Data.DataAccess;
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
            context.Deportes.Remove(deporte);
        }

        public void ModificarDeporte(Deporte deporte)
        {
            var deporteOriginal = ObtenerDeportePorNombre(deporte.Nombre);
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
