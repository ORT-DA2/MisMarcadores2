using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisMarcadores.Repository
{
   public interface ISesionesRepository : IGenericRepository<Sesion>
    {
       Sesion ObtenerPorToken(Guid token);
       void AgregarSesion(Sesion sesion);
       void BorrarSesion(Guid token);
       bool CredencialesValidas(string nombreUsuario, string contraseña);
       Usuario ObtenerUsuarioPorToken(Guid token);
    }
}
