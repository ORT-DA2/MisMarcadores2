using MisMarcadores.Data.Entities;
using MisMarcadores.Repository;
using System;

namespace MisMarcadores.Logic
{
    public class UsuariosLogic : IUsuariosLogic
    {
        public IUsuariosRepository usuariosRepository;

        public UsuariosLogic(IUsuariosRepository usuariosRepository)
        {
            this.usuariosRepository = usuariosRepository;
        }

        public void AgregarUsuario(Usuario usuario)
        {
            usuariosRepository.Agregar(usuario);
        }
    }
}
