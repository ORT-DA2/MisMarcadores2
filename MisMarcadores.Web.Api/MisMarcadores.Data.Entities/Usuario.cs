using System;
using System.Collections.Generic;

namespace MisMarcadores.Data.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public String NombreUsuario { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String Mail { get; set; }
        public String Contraseña { get; set; }
        public bool Administrador { get; set; }
        public bool Borrado { get; set; }
        public List<Equipo> Favoritos { get; set; }

        public Usuario()
        {
            Borrado = false;
            Favoritos = new List<Equipo>();
        }

        public override bool Equals(object obj)
        {
            bool igual = false;
            if (obj is Usuario)
            {
                igual = NombreUsuario.Equals(((Usuario)obj).NombreUsuario);
            }
            return igual;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
