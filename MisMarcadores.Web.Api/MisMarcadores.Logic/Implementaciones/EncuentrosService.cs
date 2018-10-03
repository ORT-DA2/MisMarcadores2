using System;
using System.Collections.Generic;
using System.Text;
using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using MisMarcadores.Repository;
using MisMarcadores.Repository.Exceptions;

namespace MisMarcadores.Logic
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
            try
            {
                _encuentrosRepository.Insert(encuentro);
                _unitOfWork.Save();
                return encuentro.Id;
            }
            catch (RepositoryException)
            {
                throw new RepositoryException();
            }
        }

        public void BorrarEncuentro(Guid id)
        {
            throw new NotImplementedException();
        }

        public void ModificarEncuentro(Guid id, Encuentro encuentro)
        {
            throw new NotImplementedException();
        }

        public Encuentro ObtenerEncuentroPorId(Guid id)
        {
            return _encuentrosRepository.ObtenerEncuentroPorId(id);
        }

        public IEnumerable<Encuentro> ObtenerEncuentros()
        {
            return _encuentrosRepository.ObtenerEncuentros();
        }

        public IEnumerable<Encuentro> ObtenerEncuentrosDeEquipo(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
