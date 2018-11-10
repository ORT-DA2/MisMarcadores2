using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
namespace MisMarcadores.Logic
{
    public interface IParticipantesService
    {
        Guid AgregarParticipante(Participante participante);
        void AgregarFavorito(Guid idParticipante, String nombreUsuario);
        void BorrarFavorito(Guid idParticipante, String nombreUsuario);
        IEnumerable<Participante> ObtenerParticipantes();
        IEnumerable<Participante> ObtenerParticipantesPorDeporte(String deporte);
        IEnumerable<Guid> ObtenerFavoritosPorUsuario(String nombreUsuario);
        Participante ObtenerParticipantePorId(Guid id);
        Participante ObtenerParticipantePorNombre(String nombre);
        void ModificarParticipante(Guid id, Participante participante);
        void BorrarParticipante(Guid id);
    }
}
