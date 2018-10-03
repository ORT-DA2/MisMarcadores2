using System;
using System.Collections.Generic;
using System.Text;
using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using MisMarcadores.Repository;
using MisMarcadores.Repository.Exceptions;
namespace MisMarcadores.Logic.Implementaciones
{
    public class EncuentrosService : IEncuentrosService
    {
        private IUnitOfWork _unitOfWork;
        private IEncuentrosRepository _encuentrosRepository;
        
        public EncuentrosService(IUnitOfWork unitOfWork, IEncuentrosRepository encuentrosRepository)
        {
            _unitOfWork = unitOfWork;
            _encuentrosRepository = encuentrosRepository;
        }

        public Guid AgregarEncuentro(Encuentro encuentro)
        {
            throw new NotImplementedException();
        }

        public void BorrarEncuentro(Guid id)
        {
            throw new NotImplementedException();
        }

        public void ModificarEncuentro(Guid id, Encuentro encuentro)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Encuentro> ObtenerEncuentros()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Encuentro> ObtenerEncuentrosDeEquipo(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
