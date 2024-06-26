using System.ComponentModel.DataAnnotations;

namespace BloggieWebProject.Models.Dominio
{
    public class BlogPost
    {
        public Guid Id { get; set; }

        [Required]
        public string Encabezado { get; set; }

        [Required]
        public string TituloPagina { get; set; }

        [Required]
        public string Contenido { get; set; }

        [Required]
        public string DescripcionCorta { get; set; }

        public string UrlImagenDestacada { get; set; }

        public string ManejadorUrl { get; set; }

        [Required]
        public DateTime FechaPublicacion { get; set; }

        [Required]
        public string Autor { get; set; }

        [Required]
        public bool Visible { get; set; }

        [Required]
        public ICollection<Tag> Tags { get; set; }  //Un post tendrá una colección de tags
    }
}
