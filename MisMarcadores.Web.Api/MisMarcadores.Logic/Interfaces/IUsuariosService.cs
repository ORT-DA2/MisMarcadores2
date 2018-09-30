using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;

namespace MisMarcadores.Logic
{
    public interface IUsuariosService
    {
        void AgregarUsuario(Usuario usuario);
        IEnumerable<Usuario> ObtenerUsuarios();
        Usuario ObtenerPorNombreUsuario(string nombreUsuario);
        void ModificarUsuario(string nombreUsuario, Usuario usuario);
        void BorrarUsuario(string nombreUsuario);
    }
}
