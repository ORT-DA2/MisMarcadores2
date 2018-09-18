using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisMarcadores.Data.Entities
{
    [Table("Comentarios")]
    public class Comentario
    {
        [Key]
        public Usuario Usuario { get; set; }
        public String Texto { get; set; }

        public Comentario() {
            Usuario = new Usuario();
        }
    }
}
