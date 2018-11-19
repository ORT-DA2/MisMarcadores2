using System;
using System.Collections.Generic;
using System.Text;
using MisMarcadores.Data.Entities;
using MisMarcadores.Data.DataAccess;
using MisMarcadores.Repository;

namespace MisMarcadores.Repository
{
    public class LogRepository :  GenericRepository<MisMarcadores.Data.Entities.Log>, ILogRepository
    {
        public LogRepository(MisMarcadoresContext context) : base(context) { }    
    }
}
