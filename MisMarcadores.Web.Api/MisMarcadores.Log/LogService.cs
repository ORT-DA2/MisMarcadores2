using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using MisMarcadores.Repository;


namespace MisMarcadores.Log
{
    public class LogService : ILogService
    {

        private IUnitOfWork _unitOfWork;
        private ILogRepository _logRepository;

        public LogService(IUnitOfWork unitOfWork, ILogRepository logRepository)
        {
            _unitOfWork = unitOfWork;
            _logRepository = logRepository;
            
        }
        public IEnumerable<MisMarcadores.Data.Entities.Log> ObtenerLog()
        {          
                return _logRepository.GetAll();           
        }

        public List<MisMarcadores.Data.Entities.Log> ObtenerLogEntreFechas(FilterDateTime filter)
        {
            IEnumerable<MisMarcadores.Data.Entities.Log> logs = ObtenerLog();
            List<Data.Entities.Log> LogsEnFecha = new List<Data.Entities.Log>();
            foreach (MisMarcadores.Data.Entities.Log log in logs)
            {
                int esDespuesFechaInicio = DateTime.Compare(filter.Inicio, log.Fecha);
                int esAntesFechaFin = DateTime.Compare( log.Fecha, filter.Fin);
                if (esDespuesFechaInicio == -1 && esAntesFechaFin == -1)
                    LogsEnFecha.Add(log);       
            }
            return LogsEnFecha;
        }

        public void InsertarAccion(string nombreUsuario, string accion)
        {
            MisMarcadores.Data.Entities.Log aInsertar = new MisMarcadores.Data.Entities.Log();
            aInsertar.Accion = accion;
            aInsertar.Fecha = DateTime.Now;
            aInsertar.Usuario = nombreUsuario;
            _logRepository.Insert(aInsertar);
            _unitOfWork.Save();
        }
    }

    public class FilterDateTime
    {
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
    }
}
