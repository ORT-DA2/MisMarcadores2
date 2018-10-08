using Microsoft.EntityFrameworkCore;
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

        public void BorrarEquipo(Guid id)
        {
            Equipo equipo = context.Equipos.FirstOrDefault(d => d.Id == id);
            var favoritos = context.Favoritos.Where(f => f.IdEquipo == equipo.Id);
            foreach (var favorito in favoritos)
            {
                context.Favoritos.Remove(favorito);
            }
            var encuentros = context.Encuentros.Where(e => e.EquipoLocal.Nombre.Equals(equipo.Nombre) || e.EquipoVisitante.Nombre.Equals(equipo.Nombre));
            foreach (var encuentro in encuentros)
            {
                context.Encuentros.Remove(encuentro);
            }
            context.Equipos.Remove(equipo);
        }

        public Equipo ObtenerEquipoPorDeporte(string nombreDeporte, string nombreEquipo)
        {
            return context.Equipos.Include(e => e.Deporte).FirstOrDefault(x => x.Deporte.Nombre.Equals(nombreDeporte) && x.Nombre.Equals(nombreEquipo));
        }

        public void ModificarEquipo(Equipo equipo)
        {
            var equipoOriginal = GetByID(equipo.Id);
            context.Equipos.Attach(equipoOriginal);
            var entry = context.Entry(equipoOriginal);
            entry.CurrentValues.SetValues(equipo);
        }

        public Equipo ObtenerEquipoPorId(Guid id)
        {
            return context.Equipos.Include(e => e.Deporte).FirstOrDefault(e => e.Id.Equals(id));
        }

        public Equipo ObtenerEquipoPorNombre(string nombre)
        {
            return context.Equipos.Include(e => e.Deporte).FirstOrDefault(e => e.Nombre.Equals(nombre));
        }

        public List<Equipo> ObtenerEquipos()
        {
            return context.Equipos.Include(e => e.Deporte).ToList();
        }

        public List<Equipo> ObtenerEquiposPorDeporte(string deporte)
        {
            return context.Equipos.Where(x => x.Deporte.Nombre.Equals(deporte)).ToList();
        }
    }
}
