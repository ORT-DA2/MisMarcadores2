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
            if (DatosInvalidosEncuentro(encuentro))
                throw new EncuentroDataException();

            Deporte deporte = _deportesRepository.ObtenerDeportePorNombre(encuentro.Deporte.Nombre);
            if (deporte == null)
                throw new NoExisteDeporteException();

            Equipo equipoLocal = _equiposRepository.ObtenerEquipoPorDeporte(encuentro.Deporte.Nombre, encuentro.EquipoLocal.Nombre);
            Equipo equipoVisitante = _equiposRepository.ObtenerEquipoPorDeporte(encuentro.Deporte.Nombre, encuentro.EquipoVisitante.Nombre);

            if (equipoLocal == null || equipoVisitante == null)
                throw new NoExisteEquipoException();

            if (_encuentrosRepository.ExisteEncuentroEnFecha(encuentro.FechaHora, equipoLocal.Id) ||
                _encuentrosRepository.ExisteEncuentroEnFecha(encuentro.FechaHora, equipoVisitante.Id))
                throw new ExisteEncuentroEnFecha();

            try
            {
                encuentro.EquipoLocal.Id = equipoLocal.Id;
                encuentro.EquipoVisitante.Id = equipoVisitante.Id;
                encuentro.Deporte.Id = deporte.Id;
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
            Encuentro encuentro = ObtenerEncuentroPorId(id);
            if (encuentro == null)
                throw new NoExisteEncuentroException();
            try
            {
                _encuentrosRepository.BorrarEncuentro(id);
                _unitOfWork.Save();
            }
            catch (RepositoryException)
            {
                throw new RepositoryException();
            }
        }

        public void ModificarEncuentro(Guid id, Encuentro encuentro)
        {
            if (DatosInvalidosEncuentro(encuentro))
                throw new EncuentroDataException();

            Deporte deporte = _deportesRepository.ObtenerDeportePorNombre(encuentro.Deporte.Nombre);
            if (deporte == null)
                throw new NoExisteDeporteException();

            Equipo equipoLocal = _equiposRepository.ObtenerEquipoPorDeporte(encuentro.Deporte.Nombre, encuentro.EquipoLocal.Nombre);
            Equipo equipoVisitante = _equiposRepository.ObtenerEquipoPorDeporte(encuentro.Deporte.Nombre, encuentro.EquipoVisitante.Nombre);

            if (equipoLocal == null || equipoVisitante == null)
                throw new NoExisteEquipoException();

            Encuentro encuentroActual = ObtenerEncuentroPorId(id);
            if (encuentroActual == null)
                throw new NoExisteEncuentroException();

            if (encuentro.FechaHora.Date != encuentroActual.FechaHora.Date)
            {
                if ((_encuentrosRepository.ExisteEncuentroEnFecha(encuentro.FechaHora, equipoLocal.Id) ||
               _encuentrosRepository.ExisteEncuentroEnFecha(encuentro.FechaHora, equipoVisitante.Id)))
                    throw new ExisteEncuentroEnFecha();
            }
            else
            {
                if (EquipoDistintoAlActual(equipoLocal.Nombre, encuentroActual))
                    if ((_encuentrosRepository.ExisteEncuentroEnFecha(encuentro.FechaHora, equipoLocal.Id)))
                        throw new ExisteEncuentroEnFecha();

                if (EquipoDistintoAlActual(equipoVisitante.Nombre, encuentroActual))
                    if ((_encuentrosRepository.ExisteEncuentroEnFecha(encuentro.FechaHora, equipoVisitante.Id)))
                        throw new ExisteEncuentroEnFecha();
            }

            try
            {
                encuentro = encuentroActual;
                encuentro.EquipoLocal = equipoLocal;
                encuentro.EquipoVisitante = equipoVisitante;
                encuentro.Deporte = deporte;
                _encuentrosRepository.ModificarEncuentro(encuentro);
                _unitOfWork.Save();
            }
            catch (RepositoryException)
            {
                throw new RepositoryException();
            }
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

        private bool DatosInvalidosEncuentro(Encuentro encuentro) {
            return (!CampoValido(encuentro.EquipoLocal.Nombre) ||
               !CampoValido(encuentro.EquipoVisitante.Nombre) ||
               !CampoValido(encuentro.Deporte.Nombre) ||
               (encuentro.EquipoLocal.Nombre == encuentro.EquipoVisitante.Nombre));
        }

        private bool CampoValido(string campo)
        {
            return !string.IsNullOrWhiteSpace(campo);
        }

        private bool EquipoDistintoAlActual(string nombreEquipo, Encuentro encuentroActual)
        {
            return (nombreEquipo != encuentroActual.EquipoLocal.Nombre) && (nombreEquipo != encuentroActual.EquipoVisitante.Nombre);
        }

    }
}
