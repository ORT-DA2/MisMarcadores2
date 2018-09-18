using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisMarcadores.Data.Entities
{
    [Table("Equipos")]
    public class Equipo
    {
        [Key]
        public String Nombre { get; set; }
        public String Foto { get; set; }

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