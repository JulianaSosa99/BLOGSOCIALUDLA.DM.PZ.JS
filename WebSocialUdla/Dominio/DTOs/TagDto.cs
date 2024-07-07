using System.ComponentModel.DataAnnotations;

namespace WebSocialUdla.Dominio.DTOs
{
	public class TagDto
	{
		public Guid Id { get; set; }

		[Required]
		public string Nombre { get; set; }

		[Required]
		public string DisplayNombre { get; set; }
	}
}
