using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
namespace MisMarcadores.Logic
{
    public interface IEquiposService
    {
        void AgregarEquipo(Equipo equipo);
        IEnumerable<Equipo> ObtenerEquipos();
        Equipo ObtenerEquipoPorNombre(String nombre);
        void ModificarEquipo(String nombre, Equipo equipo);
        void BorrarEquipo(String nombre);
    }
}
