using System;
using System.Collections.Generic;
using System.Text;
using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using MisMarcadores.Repository;

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
            
            _deportesRepository.Insert(deporte);
            _unitOfWork.Save();
        }

        public void BorrarDeporte(string nombre)
        {
            Deporte deporte = ObtenerDeportePorNombre(nombre);
            if (deporte == null)
                throw new NoExisteDeporteException();
            _deportesRepository.BorrarDeporte(nombre);
            _unitOfWork.Save();
        }

        public void ModificarDeporte(string nombre, Deporte deporte)
        {
            if (!CampoValido(deporte.Nombre) ||
                !CampoValido(nombre))
                throw new DeporteDataException();

            Deporte deporteActual = ObtenerDeportePorNombre(nombre);
            if (deporteActual == null)
                throw new NoExisteDeporteException();
            if (ObtenerDeportePorNombre(deporte.Nombre) != null)
                throw new ExisteDeporteException();
           
            deporte.Id = deporteActual.Id;
            _deportesRepository.ModificarDeporte(deporte);
            _unitOfWork.Save();
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
