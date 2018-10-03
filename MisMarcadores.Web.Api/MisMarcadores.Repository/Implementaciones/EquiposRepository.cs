using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisMarcadores.Repository
{
    public class EquiposRepository : GenericRepository<Equipo>, IEquiposRepository
    {
        public EquiposRepository(MisMarcadoresContext context) : base(context) { }

        public void BorrarEquipo(string nombre)
        {
            throw new NotImplementedException();
        }

        public bool ExisteEquipo(string nombreDeporte, string nombreEquipo)
        {
            throw new NotImplementedException();
        }

        public void ModificarEquipo(Equipo equipo)
        {
            throw new NotImplementedException();
        }

        public Equipo ObtenerEquipoPorNombre(string nombre)
        {
            throw new NotImplementedException();
        }
    }
}
