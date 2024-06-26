using System.ComponentModel.DataAnnotations;

namespace BloggieWebProject.Models.ViewModels
{
    public class EditarTagRequest
    {
        public Guid Id { get; set; }

        [Required]
        public string? Nombre { get; set; }

        [Required]
        public string? DisplayNombre { get; set; }



    }
}
