using System;
using System.Collections.Generic;
using System.Linq;
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
        private IParticipantesRepository _participantesRepository;
        private IEncuentrosRepository _encuentrosRepository;

        public EncuentrosService(IUnitOfWork unitOfWork, IEncuentrosRepository encuentrosRepository, IDeportesRepository deportesRepository,
            IParticipantesRepository participantesRepository)
        {
            _unitOfWork = unitOfWork;
            _deportesRepository = deportesRepository;
            _participantesRepository = participantesRepository;
            _encuentrosRepository = encuentrosRepository;
        }

        public Guid AgregarEncuentro(Encuentro encuentro)
        {
            if (DatosInvalidosEncuentro(encuentro))
                throw new EncuentroDataException();

            Deporte deporte = _deportesRepository.ObtenerDeportePorNombre(encuentro.Deporte.Nombre);
            if (deporte == null)
                throw new NoExisteDeporteException();

            ICollection<ParticipanteEncuentro> Puntajes = encuentro.ParticipanteEncuentro;
            foreach (ParticipanteEncuentro p in Puntajes)
            {
                p.Participante = _participantesRepository.ObtenerParticipantePorId(p.ParticipanteId);
                if (!p.Participante.Deporte.Equals(deporte))
                    throw new NoCoincideDeporteException();               
            }
            
            if (Puntajes.Count == 0)
                throw new NoExisteParticipanteException();

            if (Puntajes.Count < 2)              
                    throw new CantidadIncorrectaDePartcipantesException();

            if (!deporte.EsIndividual && Puntajes.Count != 2)
                throw new CantidadIncorrectaDePartcipantesException();

            if (HayPartcipanteRepetido(Puntajes))
                throw new ParticipantesRepetidoException();


            //if (_encuentrosRepository.ExisteEncuentroEnFecha(encuentro.FechaHora, participanteLocal.Id) ||
            //    _encuentrosRepository.ExisteEncuentroEnFecha(encuentro.FechaHora, participanteVisitante.Id))
            //    throw new ExisteEncuentroEnFecha();


            encuentro.ParticipanteEncuentro = Puntajes;
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

            //Participante participanteLocal = _participantesRepository.ObtenerParticipantePorDeporte(encuentro.Deporte.Nombre, encuentro.ParticipanteLocal.Nombre);
            //Participante participanteVisitante = _participantesRepository.ObtenerParticipantePorDeporte(encuentro.Deporte.Nombre, encuentro.ParticipanteVisitante.Nombre);

            //if (participanteLocal == null || participanteVisitante == null)
            //    throw new NoExisteParticipanteException();

            Encuentro encuentroActual = ObtenerEncuentroPorId(id);
            if (encuentroActual == null)
                throw new NoExisteEncuentroException();

            if (encuentro.FechaHora.Date != encuentroActual.FechaHora.Date)
            {
               // if ((_encuentrosRepository.ExisteEncuentroEnFecha(encuentro.FechaHora, participanteLocal.Id) ||
               //_encuentrosRepository.ExisteEncuentroEnFecha(encuentro.FechaHora, participanteVisitante.Id)))
               //     throw new ExisteEncuentroEnFecha();
            }
            else
            {
                //if (ParticipanteDistintoAlActual(participanteLocal.Nombre, encuentroActual))
                //    if ((_encuentrosRepository.ExisteEncuentroEnFecha(encuentro.FechaHora, participanteLocal.Id)))
                //        throw new ExisteEncuentroEnFecha();

                //if (ParticipanteDistintoAlActual(participanteVisitante.Nombre, encuentroActual))
                //    if ((_encuentrosRepository.ExisteEncuentroEnFecha(encuentro.FechaHora, participanteVisitante.Id)))
                //        throw new ExisteEncuentroEnFecha();
            }
            
            encuentro = encuentroActual;
            //encuentro.ParticipanteLocal = participanteLocal;
            //encuentro.ParticipanteVisitante = participanteVisitante;
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

            IEnumerable<Encuentro>  encuentros = _encuentrosRepository.ObtenerEncuentros();
            foreach (Encuentro e in encuentros)
            {
                foreach (ParticipanteEncuentro p in e.ParticipanteEncuentro)
                {
                    p.Participante = _participantesRepository.ObtenerParticipantePorId(p.ParticipanteId);
                }
            }

            return encuentros;
        }

        public IEnumerable<Encuentro> ObtenerEncuentrosDeParticipante(Guid id)
        {
            return _encuentrosRepository.ObtenerEncuentrosPorParticipante(id);
        }

        private bool DatosInvalidosEncuentro(Encuentro encuentro)
        {
            return !CampoValido(encuentro.Deporte.Nombre);
         
        }

        private bool CampoValido(string campo)
        {
            return !string.IsNullOrWhiteSpace(campo);
        }

      

        private bool HayPartcipanteRepetido(ICollection<ParticipanteEncuentro> Puntajes)
        {
            return Puntajes.Select(x => x.ParticipanteId).Distinct().Count() != Puntajes.Count();
        }

        public bool FixtureGenerado(DateTime fechaInicio, string deporte, string tipo)
        {
            if (!CampoValido(deporte) || !CampoValido(tipo) || (tipo != "Liga" && tipo != "Grupos"))
                throw new EncuentroDataException();
            Deporte deporteActual = _deportesRepository.ObtenerDeportePorNombre(deporte);
            if (deporteActual == null)
                throw new NoExisteDeporteException();
            List<Participante> participantes = _participantesRepository.ObtenerParticipantesPorDeporte(deporte);
            if (participantes == null || participantes.Count == 1)
                throw new NoExistenParticipantesException();
            Fixture fixture = GenerarFixture(fechaInicio, tipo, participantes);
            List<Encuentro> encuentros = fixture.GenerarFixture();
            bool generado = true;
            foreach (Encuentro encuentro in encuentros)
            {
                if (ExisteEncuentroParticipante(encuentro.FechaHora, participantes))
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

        private bool ExisteEncuentroParticipante(DateTime fecha, List<Participante> participantes)
        {
            List<Encuentro> encuentros = _encuentrosRepository.ObtenerEncuentros();
            foreach (Participante participante in participantes)
            {
                if (_encuentrosRepository.ExisteEncuentroEnFecha(fecha, participante.Id))
                    return true;
            }
            return false;
        }

        private Fixture GenerarFixture(DateTime fechaInicio, string tipo, List<Participante> participantes)
        {
            switch (tipo)
            {
                case "Liga":
                    return new FixtureLiga(fechaInicio, participantes);
                case "Grupos":
                    if (participantes.Count % 4 != 0)
                        throw new FixtureGruposDataException();
                    return new FixtureGrupos(fechaInicio, participantes);
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

        public IEnumerable<Encuentro> ObtenerEncuentrosPorParticipante(Guid id)
        {
            return _encuentrosRepository.ObtenerEncuentrosPorParticipante(id);
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
