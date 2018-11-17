using System;
using System.Collections.Generic;
using System.Text;
using MisMarcadores.Data.Entities;

namespace MisMarcadores.Log
{
    public interface ILogService
    {
        void InsertarAccion(string nombreUsuario);
        IEnumerable<MisMarcadores.Data.Entities.Log> ObtenerLog();
        List<MisMarcadores.Data.Entities.Log> ObtenerLogEntreFechas(FilterDateTime filter);
    }
}
