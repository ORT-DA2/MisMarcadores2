﻿using System;
using System.Collections.Generic;
using System.Text;
using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using MisMarcadores.Repository;

namespace MisMarcadores.Logic
{
    public class SesionesService : ISesionesService
    {
        private IUnitOfWork _unitOfWork;
        private ISesionesRepository _sesionesRepository;
        private ILogRepository _logRepository;

        public SesionesService(IUnitOfWork unitOfWork, ISesionesRepository sesionesRepository, ILogRepository logRepository)
        {
            _unitOfWork = unitOfWork;
            _sesionesRepository = sesionesRepository;
            _logRepository = logRepository;
        }

        public SesionesService(IUnitOfWork unitOfWork, ISesionesRepository sesionesRepository)
        {
            _unitOfWork = unitOfWork;
            _sesionesRepository = sesionesRepository;
        }

        public Sesion Login(string nombreUsuario, string contraseña)
        {
            Sesion nuevaSesion = null;
            bool loginValido = _sesionesRepository.CredencialesValidas(nombreUsuario, contraseña);
            if (loginValido)
            {
                nuevaSesion = new Sesion()
                {
                    NombreUsuario = nombreUsuario,
                    Token = Guid.NewGuid()
                };
                _sesionesRepository.AgregarSesion(nuevaSesion);
                _unitOfWork.Save();
            }
            return nuevaSesion;
        }

        public void Logout(Guid token)
        {
            _sesionesRepository.BorrarSesion(token);
            _unitOfWork.Save();
        }

        public Sesion ObtenerPorToken(Guid token)
        {
            return _sesionesRepository.ObtenerPorToken(token);
        }

        public Usuario ObtenerUsuarioPorToken(Guid token)
        {
            return _sesionesRepository.ObtenerUsuarioPorToken(token);
        }

      
    }
}
