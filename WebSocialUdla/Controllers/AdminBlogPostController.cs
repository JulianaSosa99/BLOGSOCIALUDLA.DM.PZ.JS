using BloggieWebProject.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BloggieWebProject.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using BloggieWebProject.Models.Dominio;

namespace BloggieWebProject.Controllers
{
    public class AdminBlogPostController : Controller
    {
        private readonly ITagRepositorio tagRepositorio;
        private readonly IBlogPostRepositorio blogPostRepositorio;

        public AdminBlogPostController(ITagRepositorio tagRepositorio, IBlogPostRepositorio blogPostRepositorio)
        {
            this.tagRepositorio = tagRepositorio;
            this.blogPostRepositorio = blogPostRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Agregar()
        {
            // Obtener las etiquetas del repositorio
            var tags = await tagRepositorio.GetAllAsync();

            var model = new AgregarBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Nombre, Value = x.Id.ToString() })
            };

            return View(model); // Devolver el modelo a la vista
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(AgregarBlogPostRequest agregarBlogPostRequest)
        {
            // Map view model to domain model
            var blogPost = new BlogPost
            {
                Encabezado = agregarBlogPostRequest.Encabezado,
                TituloPagina = agregarBlogPostRequest.TituloPagina,
                Contenido = agregarBlogPostRequest.Contenido,
                DescripcionCorta = agregarBlogPostRequest.DescripcionCorta,
                UrlImagenDestacada = agregarBlogPostRequest.UrlImagenDestacada,
                ManejadorUrl = agregarBlogPostRequest.ManejadorUrl,
                FechaPublicacion = agregarBlogPostRequest.FechaPublicacion,
                Autor = agregarBlogPostRequest.Autor,
                Visible = agregarBlogPostRequest.Visible,
            };

            // Map Tags from selected tags
            var tagSeleccionados = new List<Tag>();
            foreach (var tagSeleccionadosId in agregarBlogPostRequest.TagSeleccionado)
            {
                var selectedTagIdAsGuid = Guid.Parse(tagSeleccionadosId);
                var existingTag = await tagRepositorio.GetAsync(selectedTagIdAsGuid);

                if (existingTag != null)
                {
                    tagSeleccionados.Add(existingTag);
                }
            }

            blogPost.Tags = tagSeleccionados;

            await blogPostRepositorio.AddAsync(blogPost);

            return RedirectToAction("Lista");
        }
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var blogPosts = await blogPostRepositorio.GetAllAsync();
            if (blogPosts == null)
            {
                return View(new List<BlogPost>()); 
            }
            return View(blogPosts);
        }


        //Desde aqui cambie Pris
        [HttpGet]
        public async Task<IActionResult> Editar(Guid id)
        {
            var blogPost = await blogPostRepositorio.GetAsync(id);
            var tagsDomainModel = await tagRepositorio.GetAllAsync();

            if (blogPost != null)
            {
                //mapear el modelo de dominio en el view model
                var model = new EditarBlogPostRequest
                {
                    Id = blogPost.Id,
                    Encabezado = blogPost.Encabezado,
                    TituloPagina = blogPost.TituloPagina,
                    Contenido = blogPost.Contenido,
                    Autor = blogPost.Autor,
                    UrlImagenDestacada = blogPost.UrlImagenDestacada,
                    ManejadorUrl = blogPost.ManejadorUrl,
                    DescripcionCorta = blogPost.DescripcionCorta,
                    FechaPublicacion = blogPost.FechaPublicacion,
                    Visible = blogPost.Visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Nombre,
                        Value = x.Id.ToString()

                    }),
                    TagSeleccionado = blogPost.Tags.Select(x => x.Id.ToString()).ToArray(),
                };
                return View(model);

            }

            
           //pasar datos a la vista

            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Editar(EditarBlogPostRequest editarBlogPostRequest)
        {
           //Mapear View Model de vuelta en el domain model
            var blogPostDomainModel = new BlogPost
            {
                Id = editarBlogPostRequest.Id,
                Encabezado = editarBlogPostRequest.Encabezado,
                TituloPagina = editarBlogPostRequest.TituloPagina,
                Contenido = editarBlogPostRequest.Contenido,
                Autor = editarBlogPostRequest.Autor,
                DescripcionCorta = editarBlogPostRequest.DescripcionCorta,
                UrlImagenDestacada = editarBlogPostRequest.UrlImagenDestacada,
                FechaPublicacion = editarBlogPostRequest.FechaPublicacion,
                ManejadorUrl = editarBlogPostRequest.ManejadorUrl,
                Visible = editarBlogPostRequest.Visible
            };

            //Mapear tags en el domain model
            var selectedTags = new List<Tag>();
            foreach (var selectedTag in editarBlogPostRequest.TagSeleccionado) 
            { 
                if(Guid.TryParse(selectedTag, out var tag))
                {
                    var tagEncontrado= await tagRepositorio.GetAsync(tag);
                    if(tagEncontrado != null)
                    {
                        selectedTags.Add(tagEncontrado);
                    }
                }
            }

            blogPostDomainModel.Tags = selectedTags;
            //Enviar la informacion al repositorio para actualizar
            var blogActualizado= await blogPostRepositorio.UpdateAsync(blogPostDomainModel);
            if (blogActualizado != null) 
            {
                return RedirectToAction("Editar");
            }
            return RedirectToAction("Editar");

        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(EditarBlogPostRequest editarBlogPostRequest)
        {
           var blogPostEliminado = await blogPostRepositorio.DeleteAsync(editarBlogPostRequest.Id);

            if(blogPostEliminado != null)
            {
                return RedirectToAction("Lista");
            }
            return RedirectToAction("Editar", new {id= editarBlogPostRequest.Id});
        }
    }
}
