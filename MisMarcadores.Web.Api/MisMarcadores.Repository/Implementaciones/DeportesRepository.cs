using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisMarcadores.Repository
{
    public class DeportesRepository : GenericRepository<Deporte>, IDeportesRepository
    {
        public DeportesRepository(MisMarcadoresContext context) : base(context) { }

        public void BorrarDeporte(string nombre)
        {
            throw new NotImplementedException();
        }

        public void ModificarDeporte(Deporte deporte)
        {
            throw new NotImplementedException();
        }

        public Deporte ObtenerDeportePorNombre(string nombre)
        {
            throw new NotImplementedException();
        }
    }
}
