using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisMarcadores.Logic
{
    public interface IEncuentrosService
    {
        Guid AgregarEncuentro(Encuentro encuentro);
        IEnumerable<Encuentro> ObtenerEncuentros();
        IEnumerable<Encuentro> ObtenerEncuentrosPorDeporte(String nombre);
        IEnumerable<Encuentro> ObtenerEncuentrosPorParticipante(Guid id);
        Encuentro ObtenerEncuentroPorId(Guid id);
        IEnumerable<Encuentro> ObtenerEncuentrosDeParticipante(Guid id);
        List<Comentario> ObtenerComentarios(Guid idEncuentro);
        void AgregarComentario(Guid idEncuentro, String nombreUsuario, String texto);
        void ModificarEncuentro(Guid id, Encuentro encuentro);
        void BorrarEncuentro(Guid id);
        void BorrarTodos();
        bool FixtureGenerado(DateTime fechaInicio, String deporte, String tipo);
    }
}
