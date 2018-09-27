using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using MisMarcadores.Repository;
using MisMarcadores.Repository.Exceptions;
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

        public List<Usuario> ObtenerUsuarios()
        {
            return _usuariosRepository.ObtenerUsuarios();
        }

        public void AgregarUsuario(Usuario usuario)
        {
            if (!mailValido(usuario.Mail) ||
                !campoValido(usuario.Nombre) ||
                !campoValido(usuario.Apellido) ||
                !campoValido(usuario.Contraseña) ||
                !campoValido(usuario.NombreUsuario)
                )
                throw new UsuarioDataException();
            try
            {
                _usuariosRepository.Insertar(usuario);
                _unitOfWork.Save();
            }
            catch (UsuarioRepositoryException)
            {
                throw new ExisteUsuarioException();
            }
            
        }

        private bool campoValido(string campo)
        {
            return !string.IsNullOrWhiteSpace(campo);
        }

        private bool mailValido(string email)
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
