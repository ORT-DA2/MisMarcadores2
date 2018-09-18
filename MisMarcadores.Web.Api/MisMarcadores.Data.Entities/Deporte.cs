using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisMarcadores.Data.Entities
{
    [Table("Deportes")]
    public class Deporte
    {
        [Key]
        public String Nombre { get; set; }
        public List<Equipo> Equipos { get; set; }

        public Deporte()
        {
            Equipos = new List<Equipo>();
        }

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