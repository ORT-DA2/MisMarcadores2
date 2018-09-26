using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using MisMarcadores.Repository;
using MisMarcadores.Repository.Exceptions;
using System;
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

        public void AgregarUsuario(Usuario usuario)
        {
            if (!mailValido(usuario.Mail) ||
                !campoValido(usuario.Nombre))
                throw new UsuarioDataException();
            _usuariosRepository.Insert(usuario);
            _unitOfWork.Save();
            
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
