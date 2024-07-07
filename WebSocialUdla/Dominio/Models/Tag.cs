using System.ComponentModel.DataAnnotations;

namespace WebSocialUdla.Dominio.Models
{
	public class Tag
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		public string Nombre { get; set; }

		[Required]
		public string DisplayNombre { get; set; }
	}
}
