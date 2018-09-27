using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;

namespace MisMarcadores.Logic
{
    public interface IUsuariosService
    {
        void AgregarUsuario(Usuario usuario);
        List<Usuario> ObtenerUsuarios();
        Usuario ObtenerPorNombreUsuario(string nombreUsuario);
    }
}
