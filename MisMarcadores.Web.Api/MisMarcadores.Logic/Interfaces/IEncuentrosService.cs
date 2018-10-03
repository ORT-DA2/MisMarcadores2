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
        IEnumerable<Encuentro> ObtenerEncuentrosDeEquipo(Guid id);
        void ModificarEncuentro(Guid id, Encuentro encuentro);
        void BorrarEncuentro(Guid id);
    }
}
