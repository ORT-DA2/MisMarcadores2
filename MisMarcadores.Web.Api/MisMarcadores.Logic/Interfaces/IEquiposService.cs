using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
namespace MisMarcadores.Logic
{
    public interface IEquiposService
    {
        Guid AgregarEquipo(Equipo equipo);
        IEnumerable<Equipo> ObtenerEquipos();
        IEnumerable<Equipo> ObtenerEquiposPorDeporte(String deporte);
        Equipo ObtenerEquipoPorId(Guid id);
        Equipo ObtenerEquipoPorNombre(String nombre);
        void ModificarEquipo(Guid id, Equipo equipo);
        void BorrarEquipo(Guid id);
    }
}
