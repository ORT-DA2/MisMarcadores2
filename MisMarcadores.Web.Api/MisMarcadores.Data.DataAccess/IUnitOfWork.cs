using System;
using System.Collections.Generic;
using System.Text;

namespace MisMarcadores.Data.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}