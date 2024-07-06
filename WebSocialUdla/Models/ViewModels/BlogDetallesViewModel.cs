using BloggieWebProject.Models.Dominio;
using System.ComponentModel.DataAnnotations;
using WebSocialUdla.Models.Dominio;

namespace WebSocialUdla.Models.ViewModels
{
	public class BlogDetallesViewModel
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

		public int TotalLikes { get; set; }
        public bool Liked { get; set; }
		public string DescripcionComentario { get; set; }

		public IEnumerable<BlogComment> Comentarios { get; set; }


    }
}
