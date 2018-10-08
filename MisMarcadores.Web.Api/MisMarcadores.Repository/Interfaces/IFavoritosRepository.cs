using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisMarcadores.Repository
{
    public interface IFavoritosRepository : IGenericRepository<Favorito>
    {
        void BorrarFavorito(String nombreUsuario, Guid idEquipo);
        bool ExisteFavorito(String nombreUsuario, Guid idEquipo);
        List<Favorito> ObtenerFavoritosPorUsuario(String nombreUsuario);
    }
}
