using System;
using System.Collections.Generic;
using System.Text;

namespace MisMarcadores.Data.Entities
{
    public class Log
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; }
        public string Accion { get; set; }
    }
}
