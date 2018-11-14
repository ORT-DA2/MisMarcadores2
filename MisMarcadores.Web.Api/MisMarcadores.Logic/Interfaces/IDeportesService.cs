using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisMarcadores.Logic
{
    public interface IDeportesService
    {
        void AgregarDeporte(Deporte deporte);
        IEnumerable<Deporte> ObtenerDeportes();
        Deporte ObtenerDeportePorNombre(String nombre);
        List<ParticipanteEncuentro> RankingPorDeporte(string nombre);
        void ModificarDeporte(String nombre, Deporte deporte);
        void BorrarDeporte(String nombre);
    }
}
