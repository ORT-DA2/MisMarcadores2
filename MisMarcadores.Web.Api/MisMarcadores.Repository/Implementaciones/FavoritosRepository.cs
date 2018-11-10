using Microsoft.EntityFrameworkCore;
using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisMarcadores.Repository
{
    public class FavoritosRepository : GenericRepository<Favorito>, IFavoritosRepository
    {
        public FavoritosRepository(MisMarcadoresContext context) : base(context) { }

        public void BorrarFavorito(string nombreUsuario, Guid idParticipante)
        {
            Favorito favorito = context.Favoritos.FirstOrDefault(f => f.IdParticipante == idParticipante && f.NombreUsuario == nombreUsuario);
            context.Favoritos.Remove(favorito);
        }

        public bool ExisteFavorito(string nombreUsuario, Guid idParticipante)
        {
            return context.Favoritos.Any(f => f.NombreUsuario == nombreUsuario && f.IdParticipante == idParticipante);
        }

        public List<Favorito> ObtenerFavoritosPorUsuario(string nombreUsuario)
        {
            return context.Favoritos.Where(x => x.NombreUsuario.Equals(nombreUsuario)).ToList();
        }
    }
}
