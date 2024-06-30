using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BloggieWebProject.Models.ViewModels
{
    public class AgregarBlogPostRequest
    {
        [Required(ErrorMessage ="Ingrese un Encabezado")]
        public string Encabezado { get; set; }
        [Required(ErrorMessage = "Ingrese un Título para la Página")]
        public string TituloPagina { get; set; }
        [Required(ErrorMessage = "Ingrese el Contenido del Post")]
        public string Contenido { get; set; }
        [Required(ErrorMessage = "Ingrese una Descripción")]
        public string DescripcionCorta { get; set; }

        [Required(ErrorMessage = "Ingrese la URL")]
        public string UrlImagenDestacada { get; set; }

        [Required(ErrorMessage = "Ingrese la URL")]
        public string ManejadorUrl { get; set; }

        [Required(ErrorMessage = "Seleccione la Fecha de Publicación")]
        public DateTime FechaPublicacion { get; set; }

        [Required(ErrorMessage = "Ingrese un Autor")]
        public string Autor { get; set; }
        public bool Visible { get; set; }

      
        // Display tags
        public IEnumerable<SelectListItem> Tags { get; set; }

        // Collect Tag
        public string[] TagSeleccionado { get; set; } = Array.Empty<string>();

    }
}
