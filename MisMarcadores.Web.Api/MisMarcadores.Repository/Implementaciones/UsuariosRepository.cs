using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using MisMarcadores.Repository.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisMarcadores.Repository
{
    public class UsuariosRepository : GenericRepository<Usuario>, IUsuariosRepository
    {
        public UsuariosRepository(MisMarcadoresContext context) : base(context) {}

        public Usuario ObtenerPorNombreUsuario(string nombreUsuario)
        {
            return context.Usuarios.FirstOrDefault(x => x.NombreUsuario.Equals(nombreUsuario));
        }

        public void ModificarUsuario(Usuario usuario)
        {
            var usuarioOriginal = ObtenerPorNombreUsuario(usuario.NombreUsuario);
            context.Usuarios.Attach(usuarioOriginal);
            var entry = context.Entry(usuarioOriginal);
            entry.CurrentValues.SetValues(usuario);
        }

        public void BorrarUsuario(Guid id) {
            Usuario usuario = context.Usuarios.FirstOrDefault(u => u.Id == id);
            var favoritos = context.Favoritos.Where(f => f.NombreUsuario == usuario.NombreUsuario);
            foreach (var favorito in favoritos)
            {
                context.Favoritos.Remove(favorito);
            }
            var comentarios = context.Comentarios.Where(f => f.NombreUsuario == usuario.NombreUsuario);
            foreach (var comentario in comentarios)
            {
                context.Comentarios.Remove(comentario);
            }
            context.Usuarios.Remove(usuario);
        }


    }
}