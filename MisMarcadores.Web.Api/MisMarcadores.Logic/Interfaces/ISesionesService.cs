using MisMarcadores.Data.Entities;
using System;

namespace MisMarcadores.Logic
{
    public interface ISesionesService
    {
        Sesion ObtenerPorToken(Guid token);
        Usuario ObtenerUsuarioPorToken(Guid token);
        Sesion Login(string nombreUsuario, string contraseña);
        void Logout(Guid token);
    }
}
