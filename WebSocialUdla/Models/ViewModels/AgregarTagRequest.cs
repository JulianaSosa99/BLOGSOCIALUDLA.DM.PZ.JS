using System.ComponentModel.DataAnnotations;

namespace BloggieWebProject.Models.ViewModels
{
    public class AgregarTagRequest
    {
        [Required(ErrorMessage ="Ingrese el Nombre del Tag")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese el Nombre con el que se verá el Tag")]
        public string DisplayNombre { get; set; }
    }
}
