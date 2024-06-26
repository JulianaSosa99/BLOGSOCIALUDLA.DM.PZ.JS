using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BloggieWebProject.Models.ViewModels
{
    public class EditarBlogPostRequest
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

        // Display tags
        public IEnumerable<SelectListItem> Tags { get; set; }

        // Collect Tag
        public string[] TagSeleccionado { get; set; } = Array.Empty<string>();
    }
}
