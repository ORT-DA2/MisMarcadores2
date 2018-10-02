using System;
using System.Collections.Generic;
using System.Text;
using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using MisMarcadores.Repository;
using MisMarcadores.Repository.Exceptions;

namespace MisMarcadores.Logic
{
    public class DeportesService : IDeportesService
    {
        IUnitOfWork _unitOfWork;
        IDeportesRepository _deportesRepository;

        public DeportesService(IUnitOfWork unitOfWork, IDeportesRepository deportesRepository)
        {
            _unitOfWork = unitOfWork;
            _deportesRepository = deportesRepository;
        }

        public void AgregarDeporte(Deporte deporte)
        {
            if (!CampoValido(deporte.Nombre))
                throw new DeporteDataException();

            if (_deportesRepository.ObtenerDeportePorNombre(deporte.Nombre) != null)
                throw new ExisteDeporteException();
            try
            {
                _deportesRepository.Insert(deporte);
                _unitOfWork.Save();
            }
            catch (RepositoryException)
            {
                throw new RepositoryException();
            }
        }

        public void BorrarDeporte(string nombre)
        {
            Deporte deporte = ObtenerDeportePorNombre(nombre);
            if (deporte == null)
                throw new NoExisteDeporteException();
            try
            {
                _deportesRepository.BorrarDeporte(nombre);
                _unitOfWork.Save();
            }
            catch (RepositoryException)
            {
                throw new RepositoryException();
            }
        }

        public void ModificarDeporte(string nombre, Deporte deporte)
        {
            if (!CampoValido(deporte.Nombre) ||
                !CampoValido(nombre))
                throw new DeporteDataException();

            Deporte deporteActual = ObtenerDeportePorNombre(nombre);
            if (deporteActual == null)
                throw new NoExisteDeporteException();
            try
            {
                _deportesRepository.ModificarDeporte(deporte);
                _unitOfWork.Save();
            }
            catch (RepositoryException)
            {
                throw new RepositoryException();
            }
        }

        public Deporte ObtenerDeportePorNombre(string nombre)
        {
            return _deportesRepository.ObtenerDeportePorNombre(nombre);
        }

        public IEnumerable<Deporte> ObtenerDeportes()
        {
            return _deportesRepository.GetAll();
        }

        private bool CampoValido(string campo)
        {
            return !string.IsNullOrWhiteSpace(campo);
        }
    }
}
