using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisMarcadores.Repository
{
    public interface IEncuentrosRepository : IGenericRepository<Encuentro>
    {
        Encuentro ObtenerEncuentroPorId(Guid id);
        void AgregarComentario(Comentario comentario);
        void ModificarEncuentro(Encuentro encuentro);
        void BorrarEncuentro(Guid id);
        void BorrarTodos();
        List<Encuentro> ObtenerEncuentros();
        List<Encuentro> ObtenerEncuentrosPorDeporte(String nombre);
        List<Encuentro> ObtenerEncuentrosPorEquipo(Guid id);
        List<Comentario> ObtenerComentarios(Guid idEncuentro);
        bool ExisteEncuentroEnFecha(DateTime fecha, Guid idEquipo);
    }
}
