using System;
using System.Collections.Generic;
using System.Text;
using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using MisMarcadores.Repository;

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

            encuentro.EquipoLocal.Id = equipoLocal.Id;
            encuentro.EquipoVisitante.Id = equipoVisitante.Id;
            encuentro.Deporte.Id = deporte.Id;
            _encuentrosRepository.Insert(encuentro);
            _unitOfWork.Save();
            return encuentro.Id;
        }

        public void BorrarEncuentro(Guid id)
        {
            Encuentro encuentro = ObtenerEncuentroPorId(id);
            if (encuentro == null)
                throw new NoExisteEncuentroException();
            
            _encuentrosRepository.BorrarEncuentro(id);
            _unitOfWork.Save();
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
            
            encuentro = encuentroActual;
            encuentro.EquipoLocal = equipoLocal;
            encuentro.EquipoVisitante = equipoVisitante;
            encuentro.Deporte = deporte;
            _encuentrosRepository.ModificarEncuentro(encuentro);
            _unitOfWork.Save();
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
            return _encuentrosRepository.ObtenerEncuentrosPorEquipo(id);
        }

        private bool DatosInvalidosEncuentro(Encuentro encuentro)
        {
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

        public bool FixtureGenerado(DateTime fechaInicio, string deporte, string tipo)
        {
            if (!CampoValido(deporte) || !CampoValido(tipo) || (tipo != "Liga" && tipo != "Grupos"))
                throw new EncuentroDataException();
            Deporte deporteActual = _deportesRepository.ObtenerDeportePorNombre(deporte);
            if (deporteActual == null)
                throw new NoExisteDeporteException();
            List<Equipo> equipos = _equiposRepository.ObtenerEquiposPorDeporte(deporte);
            if (equipos == null || equipos.Count == 1)
                throw new NoExistenEquiposException();
            Fixture fixture = GenerarFixture(fechaInicio, tipo, equipos);
            List<Encuentro> encuentros = fixture.GenerarFixture();
            bool generado = true;
            foreach (Encuentro encuentro in encuentros)
            {
                if (ExisteEncuentroEquipo(encuentro.FechaHora, equipos))
                    generado = false;
                    break;
            }
            if (generado)
            {
                foreach (Encuentro encuentro in encuentros)
                {
                    encuentro.Deporte = deporteActual;
                    _encuentrosRepository.Insert(encuentro);
                    _unitOfWork.Save();
                }
            }
            return generado;
        }

        private bool ExisteEncuentroEquipo(DateTime fecha, List<Equipo> equipos)
        {
            List<Encuentro> encuentros = _encuentrosRepository.ObtenerEncuentros();
            foreach (Equipo equipo in equipos)
            {
                if (_encuentrosRepository.ExisteEncuentroEnFecha(fecha, equipo.Id))
                    return true;
            }
            return false;
        }

        private Fixture GenerarFixture(DateTime fechaInicio, string tipo, List<Equipo> equipos)
        {
            switch (tipo)
            {
                case "Liga":
                    return new FixtureLiga(fechaInicio, equipos);
                case "Grupos":
                    if (equipos.Count % 4 != 0)
                        throw new FixtureGruposDataException();
                    return new FixtureGrupos(fechaInicio, equipos);
            }
            return null;
        }

        public void BorrarTodos()
        {
            _encuentrosRepository.BorrarTodos();
            _unitOfWork.Save();
        }

        public IEnumerable<Encuentro> ObtenerEncuentrosPorDeporte(string nombre)
        {
            return _encuentrosRepository.ObtenerEncuentrosPorDeporte(nombre);
        }

        public IEnumerable<Encuentro> ObtenerEncuentrosPorEquipo(Guid id)
        {
            return _encuentrosRepository.ObtenerEncuentrosPorEquipo(id);
        }

        public void AgregarComentario(Guid idEncuentro, string nombreUsuario, string texto)
        {
            Encuentro encuentroActual = ObtenerEncuentroPorId(idEncuentro);
            if (encuentroActual == null)
                throw new NoExisteEncuentroException();

            Comentario comentario = new Comentario
            {
                IdEncuentro = idEncuentro,
                NombreUsuario = nombreUsuario,
                Texto = texto
            };
            _encuentrosRepository.AgregarComentario(comentario);
            _unitOfWork.Save();

        }

        public List<Comentario> ObtenerComentarios(Guid idEncuentro)
        {
            return _encuentrosRepository.ObtenerComentarios(idEncuentro);
        }
    }
}
