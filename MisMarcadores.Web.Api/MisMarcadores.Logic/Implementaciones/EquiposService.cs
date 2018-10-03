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

        public Guid AgregarEquipo(Equipo equipo)
        {
            if (!CampoValido(equipo.Nombre) ||
                !CampoValido(equipo.Deporte.Nombre))
                throw new EquipoDataExceptiom();

            Deporte deporte = _deportesRepository.ObtenerDeportePorNombre(equipo.Deporte.Nombre);
            if (deporte == null)
                throw new NoExisteDeporteException();

            if (_equiposRepository.ExisteEquipo(equipo.Deporte.Nombre, equipo.Nombre))
                throw new ExisteEquipoException();

            try
            {
                equipo.Deporte.Id = deporte.Id;
                equipo.Deporte.Nombre = deporte.Nombre;
                _equiposRepository.Insert(equipo);
                _unitOfWork.Save();
                return equipo.Id;
            }
            catch (RepositoryException)
            {
                throw new RepositoryException();
            }
        }

        public void BorrarEquipo(Guid id)
        {
            Equipo equipo = ObtenerEquipoPorId(id);
            if (equipo == null)
                throw new NoExisteEquipoException();
            try
            {
                _equiposRepository.BorrarEquipo(id);
                _unitOfWork.Save();
            }
            catch (RepositoryException)
            {
                throw new RepositoryException();
            }
        }

        public void ModificarEquipo(Guid id, Equipo equipo)
        {
            if (!CampoValido(equipo.Nombre))
                throw new EquipoDataExceptiom();

            Equipo equipoActual = ObtenerEquipoPorId(id);
            if (equipoActual == null)
                throw new NoExisteEquipoException();
            if (_equiposRepository.ExisteEquipo(equipo.Deporte.Nombre, equipo.Nombre))
                throw new ExisteEquipoException();

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

        public Equipo ObtenerEquipoPorId(Guid id)
        {
            return _equiposRepository.ObtenerEquipoPorId(id);
        }

        public IEnumerable<Equipo> ObtenerEquipos()
        {
            return _equiposRepository.ObtenerEquipos();
        }

        private bool CampoValido(string campo)
        {
            return !string.IsNullOrWhiteSpace(campo);
        }
    }
}
