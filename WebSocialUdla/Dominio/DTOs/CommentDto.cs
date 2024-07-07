namespace WebSocialUdla.Dominio.DTOs
{
	public class CommentDto
	{
		public Guid Id { get; set; }

		public string Contenido { get; set; }

		public DateTime Fecha { get; set; }

		public Guid? BlogFicaId { get; set; }

		public Guid? BlogNodoId { get; set; }
	}
}
