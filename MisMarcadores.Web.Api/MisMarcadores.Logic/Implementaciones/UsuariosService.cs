using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using MisMarcadores.Repository;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace MisMarcadores.Logic
{
    public class UsuariosService : IUsuariosService
    {
        IUnitOfWork _unitOfWork;
        IUsuariosRepository _usuariosRepository;

        public UsuariosService(IUnitOfWork unitOfWork, IUsuariosRepository usuariosRepository)
        {
            _unitOfWork = unitOfWork;
            _usuariosRepository = usuariosRepository;
        }

        public IEnumerable<Usuario> ObtenerUsuarios()
        {
            return _usuariosRepository.GetAll();
        }

        public Usuario ObtenerPorNombreUsuario(string nombreUsuario)
        {
            return _usuariosRepository.ObtenerPorNombreUsuario(nombreUsuario);
        }

        public void AgregarUsuario(Usuario usuario)
        {
            if (!MailValido(usuario.Mail) ||
                !CampoValido(usuario.Nombre) ||
                !CampoValido(usuario.Apellido) ||
                !CampoValido(usuario.Contraseña) ||
                !CampoValido(usuario.NombreUsuario)
                )
                throw new UsuarioDataException();

            if (_usuariosRepository.ObtenerPorNombreUsuario(usuario.NombreUsuario)!=null)
                throw new ExisteUsuarioException();
            
            _usuariosRepository.Insert(usuario);
            _unitOfWork.Save();
            
        }

        public void ModificarUsuario(string nombreUsuario, Usuario usuario)
        {
            if (!CampoValido(usuario.NombreUsuario) ||
                !MailValido(usuario.Mail) ||
                !CampoValido(usuario.Nombre) ||
                !CampoValido(usuario.Apellido) ||
                !CampoValido(usuario.Contraseña)
                )
                throw new UsuarioDataException();

            Usuario usuarioActual = ObtenerPorNombreUsuario(nombreUsuario);
            if (usuarioActual == null)
                throw new NoExisteUsuarioException();
            if (usuarioActual.NombreUsuario != usuario.NombreUsuario)
                throw new UsuarioDataException();
            
            usuario.Id = usuarioActual.Id;
            _usuariosRepository.ModificarUsuario(usuario);
            _unitOfWork.Save();
        }

        public void BorrarUsuario(string nombreUsuario)
        {
            Usuario usuario = ObtenerPorNombreUsuario(nombreUsuario);
            if (usuario == null)
                throw new NoExisteUsuarioException();

            _usuariosRepository.BorrarUsuario(usuario.Id);
            _unitOfWork.Save();
        }

        private bool CampoValido(string campo)
        {
            return !string.IsNullOrWhiteSpace(campo);
        }

        private bool MailValido(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
