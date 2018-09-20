using System;

namespace MisMarcadores.Data.Entities
{
    public class Equipo
    {
        public Guid Id { get; set; }
        public String Nombre { get; set; }
        public String Foto { get; set; }
        public Deporte Deporte { get; set; }

        public Equipo() {
            Deporte = new Deporte();
        }

        public override bool Equals(object obj)
        {
            bool igual = false;
            if (obj is Equipo)
            {
                igual = Nombre.Equals(((Equipo)obj).Nombre);
            }
            return igual;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}