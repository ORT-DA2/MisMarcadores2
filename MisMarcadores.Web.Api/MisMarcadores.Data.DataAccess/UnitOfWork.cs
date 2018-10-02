using System;

namespace MisMarcadores.Data.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private MisMarcadoresContext context;

        public UnitOfWork(MisMarcadoresContext context) {
            this.context = context;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}