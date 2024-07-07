using System.ComponentModel.DataAnnotations;

namespace WebSocialUdla.Dominio.DTOs
{
	public class BlogFicaDto
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

		[Required]
		public DateTime FechaPublicacion { get; set; }

		[Required]
		public string Autor { get; set; }

		[Required]
		public bool Visible { get; set; }

		public ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();

		public ICollection<TagDto> Tags { get; set; } = new List<TagDto>();
		public string[] TagSeleccionado { get; internal set; }
	}
}
