using System;
using System.Collections.Generic;
using System.Text;
using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using MisMarcadores.Repository;
namespace MisMarcadores.Logic
{
    public class ParticipantesService : IParticipantesService
    {
        private IUnitOfWork _unitOfWork;
        private IParticipantesRepository _participantesRepository;
        private IDeportesRepository _deportesRepository;
        private IFavoritosRepository _favoritosRepository;

        public ParticipantesService(IUnitOfWork unitOfWork, IParticipantesRepository participantesRepository, 
            IDeportesRepository deportesRepository, IFavoritosRepository favoritosRepository)
        {
            _unitOfWork = unitOfWork;
            _participantesRepository = participantesRepository;
            _deportesRepository = deportesRepository;
            _favoritosRepository = favoritosRepository;
        }

        public Guid AgregarParticipante(Participante participante)
        {
            if (!CampoValido(participante.Nombre) ||
                !CampoValido(participante.Deporte.Nombre))
                throw new ParticipanteDataException();

            Deporte deporte = _deportesRepository.ObtenerDeportePorNombre(participante.Deporte.Nombre);
            if (deporte == null)
                throw new NoExisteDeporteException();

            if (_participantesRepository.ObtenerParticipantePorDeporte(participante.Deporte.Nombre, participante.Nombre) !=null)
                throw new ExisteParticipanteException();

            if (participante.EsEquipo == deporte.EsIndividual)
                throw new ParticipanteDistintoTipoDeporteException();

            if (participante.Foto != null) {
                try {
                    string[] pd = participante.Foto.Split(',');
                    Convert.FromBase64String(pd[1]);
                }
                catch (FormatException e) {
                    throw e;
                }
            }

            participante.Deporte.Id = deporte.Id;
            _participantesRepository.Insert(participante);
            _unitOfWork.Save();
            return participante.Id;
        }

        public void AgregarFavorito(Guid idParticipante, string nombreUsuario)
        {
            Participante participante = ObtenerParticipantePorId(idParticipante);
            if (participante == null)
                throw new NoExisteParticipanteException();
            if (_favoritosRepository.ExisteFavorito(nombreUsuario, idParticipante))
                throw new ExisteFavoritoException();

            Favorito favorito = new Favorito
            {
                IdParticipante = idParticipante,
                NombreUsuario = nombreUsuario
            };
            _favoritosRepository.Insert(favorito);
            _unitOfWork.Save();
        }

        public void BorrarFavorito(Guid idParticipante, string nombreUsuario)
        {
            Participante participante = ObtenerParticipantePorId(idParticipante);
            if (participante == null)
                throw new NoExisteParticipanteException();
            if (!_favoritosRepository.ExisteFavorito(nombreUsuario, idParticipante))
                throw new NoExisteFavoritoException();
          
            _favoritosRepository.BorrarFavorito(nombreUsuario, idParticipante);
            _unitOfWork.Save();
        }

        public void BorrarParticipante(Guid id)
        {
            Participante participante = ObtenerParticipantePorId(id);
            if (participante == null)
                throw new NoExisteParticipanteException();
            _participantesRepository.BorrarParticipante(id);
            _unitOfWork.Save();
        }

        public void ModificarParticipante(Guid id, Participante participante)
        {
            if (!CampoValido(participante.Nombre))
                throw new ParticipanteDataException();

            Participante participanteActual = ObtenerParticipantePorId(id);
            if (participanteActual == null)
                throw new NoExisteParticipanteException();
            if (_participantesRepository.ObtenerParticipantePorDeporte(participante.Deporte.Nombre, participante.Nombre) != null)
                throw new ExisteParticipanteException();
            if (participante.Foto != null)
            {
                try
                {
                    string[] pd = participante.Foto.Split(',');
                    Convert.FromBase64String(pd[1]);
                }
                catch (FormatException e)
                {
                    throw e;
                }
            }
            participante.Id = participanteActual.Id;
            _participantesRepository.ModificarParticipante(participante);
            _unitOfWork.Save();
        }

        public Participante ObtenerParticipantePorId(Guid id)
        {
            return _participantesRepository.ObtenerParticipantePorId(id);
        }

        public Participante ObtenerParticipantePorNombre(string nombre)
        {
            return _participantesRepository.ObtenerParticipantePorNombre(nombre);
        }

        public IEnumerable<Participante> ObtenerParticipantes()
        {
            return _participantesRepository.ObtenerParticipantes();
        }

        public IEnumerable<Participante> ObtenerParticipantesPorDeporte(string deporte)
        {
            return _participantesRepository.ObtenerParticipantesPorDeporte(deporte);
        }

        public IEnumerable<Guid> ObtenerFavoritosPorUsuario(string nombreUsuario)
        {
            List<Guid> idParticipantes = new List<Guid>();
            List<Favorito> favoritos = _favoritosRepository.ObtenerFavoritosPorUsuario(nombreUsuario);
            foreach (Favorito favorito in favoritos)
            {
                if (favorito.NombreUsuario.Equals(nombreUsuario))
                    idParticipantes.Add(favorito.IdParticipante);
            }
            return idParticipantes;
        }

        private bool CampoValido(string campo)
        {
            return !string.IsNullOrWhiteSpace(campo);
        }
    }
}
