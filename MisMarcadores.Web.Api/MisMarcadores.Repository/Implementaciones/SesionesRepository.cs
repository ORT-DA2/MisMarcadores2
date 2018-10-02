using MisMarcadores.Data.DataAccess;
using MisMarcadores.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisMarcadores.Repository
{
    public class SesionesRepository : GenericRepository<Sesion>, ISesionesRepository
    {
        public SesionesRepository(MisMarcadoresContext context) : base(context) { }

        public Sesion ObtenerPorToken(Guid token) {
            return context.Sesiones.FirstOrDefault(s => s.Token == token);
        }
        public void AgregarSesion(Sesion sesion) {
            Sesion sesionExistente = context.Sesiones.FirstOrDefault(s => s.NombreUsuario == sesion.NombreUsuario);
            if (sesionExistente == null)
            {
                context.Sesiones.Add(sesion);
            }
            else
            {
                sesionExistente.Token = sesion.Token;
            }
        }
        public void BorrarSesion(Guid token) {
            Sesion sesion = context.Sesiones.FirstOrDefault(s => s.Token == token);
            context.Sesiones.Remove(sesion);
        }
        public bool CredencialesValidas(string nombreUsuario, string contraseña) {
            return context.Usuarios.Any(u => u.NombreUsuario == nombreUsuario && u.Contraseña == contraseña);
        }
        public Usuario ObtenerUsuarioPorToken(Guid token) {
            Sesion sesion = context.Sesiones.FirstOrDefault(s => s.Token == token);
            return context.Usuarios.FirstOrDefault(x => x.NombreUsuario.Equals(sesion.NombreUsuario));
        }
    }
}
