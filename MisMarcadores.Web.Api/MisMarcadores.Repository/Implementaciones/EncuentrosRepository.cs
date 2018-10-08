using Microsoft.EntityFrameworkCore;
using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisMarcadores.Repository
{
    public class EncuentrosRepository : GenericRepository<Encuentro>, IEncuentrosRepository
    {
        public EncuentrosRepository(MisMarcadoresContext context) : base(context) { }

        public void BorrarEncuentro(Guid id)
        {
            Encuentro encuentro = context.Encuentros.FirstOrDefault(e => e.Id == id);
            context.Encuentros.Remove(encuentro);
        }

        public void BorrarTodos()
        {
            foreach (Encuentro encuentro in context.Encuentros)
            {
                context.Encuentros.Remove(encuentro);
            }
        }

        public bool ExisteEncuentroEnFecha(DateTime fecha, Guid idEquipo)
        {
            return context.Encuentros.Any(x => (x.EquipoLocal.Id == idEquipo || x.EquipoVisitante.Id == idEquipo ) && x.FechaHora.Date.Equals(fecha.Date));
        }

        public void ModificarEncuentro(Encuentro encuentro)
        {
            var encuentroOriginal = GetByID(encuentro.Id);
            context.Encuentros.Attach(encuentroOriginal);
            var entry = context.Entry(encuentroOriginal);
            entry.CurrentValues.SetValues(encuentro);
        }

        public Encuentro ObtenerEncuentroPorId(Guid id)
        {
            return context.Encuentros.Include(e => e.Deporte).Include(e => e.EquipoLocal).Include(e => e.EquipoVisitante).FirstOrDefault(e => e.Id.Equals(id));
        }

        public List<Encuentro> ObtenerEncuentros()
        {
            return context.Encuentros.Include(e => e.Deporte).Include(e => e.EquipoLocal).Include(e => e.EquipoVisitante).ToList();
        }
    }
}
