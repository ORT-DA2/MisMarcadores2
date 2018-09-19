using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisMarcadores.Repository
{
    public interface IUsuariosRepository
    {
        IEnumerable<Usuario> ObtenerUsuarios();
        Usuario ObtenerPorNombreUsuario(string nombreUsuario);
        bool Agregar(Usuario usuario);
        bool Borrar(string nombreUsuario);
        bool Modificar(string nombreUsuario, Usuario nuevoUsuario);
    }
}
