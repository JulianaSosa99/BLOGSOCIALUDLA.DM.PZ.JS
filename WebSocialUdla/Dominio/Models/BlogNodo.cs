using System.ComponentModel.DataAnnotations;

namespace WebSocialUdla.Dominio.Models
{
	public class BlogNodo
	{
		[Key]
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

		public DateTime FechaPublicacion { get; set; }

		[Required]
		public string Autor { get; set; }

		[Required]
		public bool Visible { get; set; }

		public ICollection<Tag> Tags { get; set; } = new List<Tag>();

		public ICollection<Comment> Comments { get; set; } = new List<Comment>();
	}
}
