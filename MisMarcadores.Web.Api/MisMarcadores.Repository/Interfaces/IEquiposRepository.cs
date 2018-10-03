using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisMarcadores.Repository
{
    public interface IEquiposRepository : IGenericRepository<Equipo>
    {
        Deporte ObtenerEquipoPorNombre(String nombre);
        void ModificarEquipo(Equipo equipo);
        void BorrarEquipo(String nombre);
    }
}
