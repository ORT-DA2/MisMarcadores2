using System;

namespace MisMarcadores.Data.Entities
{
    public class Sesion
    {
        public Guid Id { get; set; }
        public String NombreUsuario { get; set; }
        public Guid Token { get; set; }

        public override bool Equals(object obj)
        {
            bool igual = false;
            if (obj is Sesion)
            {
                igual = NombreUsuario.Equals(((Sesion)obj).NombreUsuario)
                    && Token.Equals(((Sesion)obj).Token);
            }
            return igual;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}