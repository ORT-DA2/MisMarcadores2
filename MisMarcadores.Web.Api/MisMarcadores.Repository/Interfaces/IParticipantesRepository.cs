using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisMarcadores.Repository
{
    public interface IParticipantesRepository : IGenericRepository<Participante>
    {
        Participante ObtenerParticipantePorId(Guid id);
        Participante ObtenerParticipantePorNombre(String nombre);
        Participante ObtenerParticipantePorDeporte(String nombreDeporte, String nombreParticipante);
        void ModificarParticipante(Participante participante);
        void BorrarParticipante(Guid id);
        List<Participante> ObtenerParticipantes();
        List<Participante> ObtenerParticipantesPorDeporte(String deporte);
    }
}
