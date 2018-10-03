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
        private IDeportesRepository _deportesRepository;
        private IEquiposRepository _equiposRepository;
        private IEncuentrosRepository _encuentrosRepository;
        
        public EncuentrosService(IUnitOfWork unitOfWork, IEncuentrosRepository encuentrosRepository, IDeportesRepository deportesRepository,
            IEquiposRepository equiposRepository)
        {
            _unitOfWork = unitOfWork;
            _deportesRepository = deportesRepository;
            _equiposRepository = equiposRepository;
            _encuentrosRepository = encuentrosRepository;
        }

        public Guid AgregarEncuentro(Encuentro encuentro)
        {
            if (!CampoValido(encuentro.EquipoLocal.Nombre) ||
                !CampoValido(encuentro.EquipoLocal.Deporte.Nombre) ||
                !CampoValido(encuentro.EquipoVisitante.Nombre) ||
                !CampoValido(encuentro.EquipoVisitante.Deporte.Nombre) ||
                !CampoValido(encuentro.Deporte.Nombre))
                    throw new EncuentroDataException();

            Deporte deporte = _deportesRepository.ObtenerDeportePorNombre(encuentro.Deporte.Nombre);
            if (deporte == null)
                throw new NoExisteDeporteException();

            if (!_equiposRepository.ExisteEquipo(encuentro.Deporte.Nombre, encuentro.EquipoLocal.Nombre) ||
                !_equiposRepository.ExisteEquipo(encuentro.Deporte.Nombre, encuentro.EquipoVisitante.Nombre))
                throw new NoExisteEquipoException();

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

        private bool CampoValido(string campo)
        {
            return !string.IsNullOrWhiteSpace(campo);
        }
    }
}
