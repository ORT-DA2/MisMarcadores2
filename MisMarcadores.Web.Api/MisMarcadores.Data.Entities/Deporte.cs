using System;

namespace MisMarcadores.Data.Entities
{
    public class Deporte
    {
        public Guid Id { get; set; }
        public String Nombre { get; set; }

        public override bool Equals(object obj)
        {
            bool igual = false;
            if (obj is Deporte)
            {
                igual = Nombre.Equals(((Deporte)obj).Nombre);
            }
            return igual;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}