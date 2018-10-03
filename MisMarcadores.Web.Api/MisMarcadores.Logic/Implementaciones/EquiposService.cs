using System;
using System.Collections.Generic;
using System.Text;
using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using MisMarcadores.Repository;
using MisMarcadores.Repository.Exceptions;
namespace MisMarcadores.Logic
{
    public class EquiposService : IEquiposService
    {
        private IUnitOfWork _unitOfWork;
        private IEquiposRepository _equiposRepository;
        private IDeportesRepository _deportesRepository;

        public EquiposService(IUnitOfWork unitOfWork, IEquiposRepository equiposRepository, IDeportesRepository deportesRepository)
        {
            _unitOfWork = unitOfWork;
            _equiposRepository = equiposRepository;
            _deportesRepository = deportesRepository;
        }

        public void AgregarEquipo(Equipo equipo)
        {
            if (!CampoValido(equipo.Nombre) ||
                !CampoValido(equipo.Deporte.Nombre))
                throw new EquipoDataExceptiom();
            
            if (_deportesRepository.ObtenerDeportePorNombre(equipo.Deporte.Nombre) == null)
                throw new NoExisteDeporteException();

            if (_equiposRepository.ExisteEquipo(equipo.Deporte.Nombre, equipo.Nombre))
                throw new ExisteEquipoException();

            try
            {
                _equiposRepository.Insert(equipo);
                _unitOfWork.Save();
            }
            catch (RepositoryException)
            {
                throw new RepositoryException();
            }
        }

        public void BorrarEquipo(string nombre)
        {
            Equipo equipo = ObtenerEquipoPorNombre(nombre);
            if (equipo == null)
                throw new NoExisteEquipoException();
            try
            {
                _equiposRepository.BorrarEquipo(nombre);
                _unitOfWork.Save();
            }
            catch (RepositoryException)
            {
                throw new RepositoryException();
            }
        }

        public void ModificarEquipo(string nombre, Equipo equipo)
        {
            if (!CampoValido(equipo.Nombre) ||
                !CampoValido(nombre))
                throw new EquipoDataExceptiom();

            Equipo equipoActual = ObtenerEquipoPorNombre(nombre);
            if (equipoActual == null)
                throw new NoExisteEquipoException();
            try
            {
                equipo.Id = equipoActual.Id;
                _equiposRepository.ModificarEquipo(equipo);
                _unitOfWork.Save();
            }
            catch (RepositoryException)
            {
                throw new RepositoryException();
            }
        }

        public Equipo ObtenerEquipoPorNombre(string nombre)
        {
            return _equiposRepository.ObtenerEquipoPorNombre(nombre);
        }

        public IEnumerable<Equipo> ObtenerEquipos()
        {
            return _equiposRepository.GetAll();
        }

        private bool CampoValido(string campo)
        {
            return !string.IsNullOrWhiteSpace(campo);
        }
    }
}
