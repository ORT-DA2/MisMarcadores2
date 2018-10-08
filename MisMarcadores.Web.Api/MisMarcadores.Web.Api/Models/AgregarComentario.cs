using System.ComponentModel.DataAnnotations;

namespace MisMarcadores.Web.Api.Models
{
    public class AgregarComentario
    {
        [Required]
        public string Comentario { get; set; }
    }
}
