using System;
using System.Collections.Generic;
using System.Text;
using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using MisMarcadores.Repository;
using MisMarcadores.Repository.Exceptions;
namespace MisMarcadores.Logic
{
    public class EquiposService : IEquiposService
    {
        public void AgregarEquipo(Equipo equipo)
        {
            throw new NotImplementedException();
        }

        public void BorrarEquipo(string nombre)
        {
            throw new NotImplementedException();
        }

        public void ModificarEquipo(string nombre, Equipo equipo)
        {
            throw new NotImplementedException();
        }

        public Equipo ObtenerEquipoPorNombre(string nombre)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Equipo> ObtenerEquipos()
        {
            throw new NotImplementedException();
        }
    }
}
