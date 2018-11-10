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

        public void AgregarComentario(Comentario comentario)
        {
            context.Comentarios.Add(comentario);
        }

        public void BorrarEncuentro(Guid id)
        {
            Encuentro encuentro = context.Encuentros.FirstOrDefault(e => e.Id == id);
            var comentarios = context.Comentarios.Where(c => c.IdEncuentro.Equals(id));
            foreach (var comentario in comentarios)
            {
                context.Comentarios.Remove(comentario);
            }
            context.Encuentros.Remove(encuentro);
        }

        public void BorrarTodos()
        {
            foreach (Encuentro encuentro in context.Encuentros)
            {
                context.Encuentros.Remove(encuentro);
            }
        }

        public bool ExisteEncuentroEnFecha(DateTime fecha, Guid idParticipante)
        {
            return context.Encuentros.Any(x => (x.ParticipanteLocal.Id == idParticipante || x.ParticipanteVisitante.Id == idParticipante ) && x.FechaHora.Date.Equals(fecha.Date));
        }

        public void ModificarEncuentro(Encuentro encuentro)
        {
            var encuentroOriginal = GetByID(encuentro.Id);
            context.Encuentros.Attach(encuentroOriginal);
            var entry = context.Entry(encuentroOriginal);
            entry.CurrentValues.SetValues(encuentro);
        }

        public List<Comentario> ObtenerComentarios(Guid idEncuentro)
        {
            return context.Comentarios.Where(c => c.IdEncuentro == idEncuentro).ToList();
        }

        public Encuentro ObtenerEncuentroPorId(Guid id)
        {
            return context.Encuentros.Include(e => e.Deporte).Include(e => e.ParticipanteLocal).Include(e => e.ParticipanteVisitante).FirstOrDefault(e => e.Id.Equals(id));
        }

        public List<Encuentro> ObtenerEncuentros()
        {
            return context.Encuentros.Include(e => e.Deporte).Include(e => e.ParticipanteLocal).Include(e => e.ParticipanteVisitante).ToList();
        }

        public List<Encuentro> ObtenerEncuentrosPorDeporte(string nombre)
        {
            return context.Encuentros.Include(e => e.Deporte).Include(e => e.ParticipanteLocal).Include(e => e.ParticipanteVisitante).Where(x => x.Deporte.Nombre.Equals(nombre)).ToList();
        }

        public List<Encuentro> ObtenerEncuentrosPorParticipante(Guid idParticipante)
        {
            return context.Encuentros.Include(e => e.Deporte).Include(e => e.ParticipanteLocal).Include(e => e.ParticipanteVisitante).Where(x => (x.ParticipanteLocal.Id == idParticipante || x.ParticipanteVisitante.Id == idParticipante)).ToList();
        }
    }
}
