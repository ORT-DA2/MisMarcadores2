using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisMarcadores.Repository
{
    public interface IUsuariosRepository : IGenericRepository<Usuario>
    {
        void Insertar(Usuario usuario);
        List<Usuario> ObtenerUsuarios();
        Usuario ObtenerPorNombreUsuario(string nombreUsuario);
    }
}
