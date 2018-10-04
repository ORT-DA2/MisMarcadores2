using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisMarcadores.Repository
{
    public interface IEncuentrosRepository : IGenericRepository<Encuentro>
    {
        Encuentro ObtenerEncuentroPorId(Guid id);
        void ModificarEncuentro(Encuentro encuentro);
        void BorrarEncuentro(Guid id);
        List<Encuentro> ObtenerEncuentros();
        bool ExisteEncuentroEnFecha(DateTime fecha, Guid idEquipo);
    }
}
