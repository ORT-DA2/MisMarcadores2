using System;
using System.Collections.Generic;
using System.Text;
using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using MisMarcadores.Repository;
namespace MisMarcadores.Logic
{
    public class EquiposService : IEquiposService
    {
        private IUnitOfWork _unitOfWork;
        private IEquiposRepository _equiposRepository;
        private IDeportesRepository _deportesRepository;
        private IFavoritosRepository _favoritosRepository;

        public EquiposService(IUnitOfWork unitOfWork, IEquiposRepository equiposRepository, 
            IDeportesRepository deportesRepository, IFavoritosRepository favoritosRepository)
        {
            _unitOfWork = unitOfWork;
            _equiposRepository = equiposRepository;
            _deportesRepository = deportesRepository;
            _favoritosRepository = favoritosRepository;
        }

        public Guid AgregarEquipo(Equipo equipo)
        {
            if (!CampoValido(equipo.Nombre) ||
                !CampoValido(equipo.Deporte.Nombre))
                throw new EquipoDataExceptiom();

            Deporte deporte = _deportesRepository.ObtenerDeportePorNombre(equipo.Deporte.Nombre);
            if (deporte == null)
                throw new NoExisteDeporteException();

            if (_equiposRepository.ObtenerEquipoPorDeporte(equipo.Deporte.Nombre, equipo.Nombre) !=null)
                throw new ExisteEquipoException();

            if (equipo.Foto != null) {
                try {
                    Convert.FromBase64String(equipo.Foto);
                }
                catch (FormatException e) {
                    throw e;
                }
            }

            equipo.Deporte.Id = deporte.Id;
            _equiposRepository.Insert(equipo);
            _unitOfWork.Save();
            return equipo.Id;
        }

        public void AgregarFavorito(Guid idEquipo, string nombreUsuario)
        {
            Equipo equipo = ObtenerEquipoPorId(idEquipo);
            if (equipo == null)
                throw new NoExisteEquipoException();
            if (_favoritosRepository.ExisteFavorito(nombreUsuario, idEquipo))
                throw new ExisteFavoritoException();

            Favorito favorito = new Favorito
            {
                IdEquipo = idEquipo,
                NombreUsuario = nombreUsuario
            };
            _favoritosRepository.Insert(favorito);
            _unitOfWork.Save();
        }

        public void BorrarFavorito(Guid idEquipo, string nombreUsuario)
        {
            Equipo equipo = ObtenerEquipoPorId(idEquipo);
            if (equipo == null)
                throw new NoExisteEquipoException();
            if (!_favoritosRepository.ExisteFavorito(nombreUsuario, idEquipo))
                throw new NoExisteFavoritoException();
          
            _favoritosRepository.BorrarFavorito(nombreUsuario, idEquipo);
            _unitOfWork.Save();
        }

        public void BorrarEquipo(Guid id)
        {
            Equipo equipo = ObtenerEquipoPorId(id);
            if (equipo == null)
                throw new NoExisteEquipoException();
            _equiposRepository.BorrarEquipo(id);
            _unitOfWork.Save();
        }

        public void ModificarEquipo(Guid id, Equipo equipo)
        {
            if (!CampoValido(equipo.Nombre))
                throw new EquipoDataExceptiom();

            Equipo equipoActual = ObtenerEquipoPorId(id);
            if (equipoActual == null)
                throw new NoExisteEquipoException();
            if (_equiposRepository.ObtenerEquipoPorDeporte(equipo.Deporte.Nombre, equipo.Nombre) != null)
                throw new ExisteEquipoException();
            if (equipo.Foto != null)
            {
                try
                {
                    Convert.FromBase64String(equipo.Foto);
                }
                catch (FormatException e)
                {
                    throw e;
                }
            }
            equipo.Id = equipoActual.Id;
            _equiposRepository.ModificarEquipo(equipo);
            _unitOfWork.Save();
        }

        public Equipo ObtenerEquipoPorId(Guid id)
        {
            return _equiposRepository.ObtenerEquipoPorId(id);
        }

        public Equipo ObtenerEquipoPorNombre(string nombre)
        {
            return _equiposRepository.ObtenerEquipoPorNombre(nombre);
        }

        public IEnumerable<Equipo> ObtenerEquipos()
        {
            return _equiposRepository.ObtenerEquipos();
        }

        public IEnumerable<Equipo> ObtenerEquiposPorDeporte(string deporte)
        {
            return _equiposRepository.ObtenerEquiposPorDeporte(deporte);
        }

        public IEnumerable<Guid> ObtenerFavoritosPorUsuario(string nombreUsuario)
        {
            List<Guid> idEquipos = new List<Guid>();
            List<Favorito> favoritos = _favoritosRepository.ObtenerFavoritosPorUsuario(nombreUsuario);
            foreach (Favorito favorito in favoritos)
            {
                if (favorito.NombreUsuario.Equals(nombreUsuario))
                    idEquipos.Add(favorito.IdEquipo);
            }
            return idEquipos;
        }

        private bool CampoValido(string campo)
        {
            return !string.IsNullOrWhiteSpace(campo);
        }
    }
}
