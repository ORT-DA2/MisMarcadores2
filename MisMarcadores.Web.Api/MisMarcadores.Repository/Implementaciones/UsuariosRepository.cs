using System;
using System.Collections.Generic;
using System.Text;
using MisMarcadores.Data.Entities;

namespace MisMarcadores.Repository.Implementaciones
{
    public class UsuariosRepository : IUsuariosRepository
    {
        public bool Agregar(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public bool Borrar(string nombreUsuario)
        {
            throw new NotImplementedException();
        }

        public bool Modificar(string nombreUsuario, Usuario nuevoUsuario)
        {
            throw new NotImplementedException();
        }

        public Usuario ObtenerPorNombreUsuario(string nombreUsuario)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> ObtenerUsuarios()
        {
            throw new NotImplementedException();
        }
    }
}
