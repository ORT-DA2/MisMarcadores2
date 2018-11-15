using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using MisMarcadores.Repository;

namespace MisMarcadores.Logic
{
    public class DeportesService : IDeportesService
    {
        private IUnitOfWork _unitOfWork;
        private IDeportesRepository _deportesRepository;
        private IEncuentrosRepository _encuentrosRepository;
        private IParticipantesRepository _participantesRepository;

        public DeportesService(IUnitOfWork unitOfWork, IDeportesRepository deportesRepository, IEncuentrosRepository encuentrosRepository, IParticipantesRepository participantesRepository)
        {
            _unitOfWork = unitOfWork;
            _deportesRepository = deportesRepository;
            _encuentrosRepository = encuentrosRepository;
            _participantesRepository = participantesRepository;
         }

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

        public List<ParticipanteEncuentro> PuntajesPorDeporte(string nombre) {
            List<ParticipanteEncuentro> puntajes = new List<ParticipanteEncuentro>();
            IEnumerable<Encuentro> encuentros = _encuentrosRepository.GetAll();
            foreach (Encuentro e in encuentros)
            {
                if (e.Deporte.Nombre.Equals(nombre))
                {
                    puntajes.AddRange(e.ParticipanteEncuentro);
                }
            }
            return puntajes;
        }

        public List<Participante> ParticipantePorDeporte(string nombre){
            List<Participante> participantesDeporte = new List<Participante>();
            IEnumerable<Participante> TodosParticipantes = _participantesRepository.ObtenerParticipantes();
            foreach (Participante p in TodosParticipantes)
            {
                if (p.Deporte.Nombre.Equals(nombre))
                {
                    participantesDeporte.Add(p);
                }
            }
            return participantesDeporte;
        }

        public List<Posicion> RankingPorDeporte(string nombre)
        {
            List<Participante> participantes = ParticipantePorDeporte (nombre);
            List<ParticipanteEncuentro> puntajes = PuntajesPorDeporte(nombre);
            List<Posicion> posiciones = new List<Posicion>();
            foreach (Participante par in participantes)
            {
                Posicion p = new Posicion()
                {};
                p.Nombre = par.Nombre;
                p.Puntos = 0;
                foreach (ParticipanteEncuentro puntaje in puntajes)
                {
                    if (puntaje.Participante.Nombre.Equals(p.Nombre))
                    {
                        p.Puntos += puntaje.PuntosObtenidos;
                    }
                }
                posiciones.Add(p);
            }
            return posiciones;
        }

        public List<Posicion> PosicionesPorDeporte(string nombre) {
            List<Posicion> posiciones = RankingPorDeporte(nombre).OrderByDescending(p => p.Puntos).ToList();
            int puesto = 1;
            foreach (Posicion item in posiciones)
            {
                item.Puesto = puesto;
                puesto++;
            }
            return posiciones;

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


        public class Posicion {

            public int Puesto;
            public string Nombre;
            public int Puntos;
        }
    }
}
