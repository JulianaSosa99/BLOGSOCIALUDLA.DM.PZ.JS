using BloggieWebProject.Data;
using BloggieWebProject.Models.Dominio;
using BloggieWebProject.Models.ViewModels;
using BloggieWebProject.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BloggieWebProject.Controllers
{
    public class AdminTagsController : Controller
    {
        public readonly ITagRepositorio tagRepositorio;

        public AdminTagsController(ITagRepositorio tagRepositorio )
        {
           this.tagRepositorio = tagRepositorio;
        }
        



        [HttpGet]
        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(AgregarTagRequest agregarTagRequest)
        {
            if (ModelState.IsValid)
            {
                var tag = new Tag
                {
                    Nombre = agregarTagRequest.Nombre,
                    DisplayNombre = agregarTagRequest.DisplayNombre
                };
                

                await tagRepositorio.AddAsync(tag);
             

                //await _blogDbContext.Tags.AddAsync(tag);
                //await _blogDbContext.SaveChangesAsync() ;

                //return RedirectToAction(nameof(Agregar));
                return RedirectToAction("Listar");
            }

            //return View(agregarTagRequest);
            return View(agregarTagRequest);
        }

        [HttpGet]
        [ActionName("Listar")]
        public async Task<IActionResult> Listar()
        {
            //Usar DbContext para leer los tags
            //var tags = await _blogDbContext.Tags.ToListAsync();
            var tags = await tagRepositorio.GetAllAsync();


            return View(tags);
        }


        [HttpGet]
        public async Task<IActionResult> Editar(Guid id)
        //    //primer metodoo
        //    // var tag = _blogDbContext.Tags.Find(id);

        //    //segundo metodo
        {
            var tag = await tagRepositorio.GetAsync(id);  

            if (tag != null)
            {
                var editarTagRequest = new EditarTagRequest
                {
                    Id = tag.Id,
                    Nombre = tag.Nombre,
                    DisplayNombre = tag.DisplayNombre
                };
                return View(editarTagRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EditarTagRequest editarTagRequest)
        {
            var tag = new Tag
            {
                Id = editarTagRequest.Id,
                Nombre = editarTagRequest.Nombre,
                DisplayNombre = editarTagRequest.DisplayNombre

            };
            var tagActualizado= await  tagRepositorio.UpdateAsync(tag);
            if (tagActualizado != null)
            {
                //mensaje correcto
                return RedirectToAction("Listar");
            }
            else {
                //Mostrar notificación de fallo
              
            }
            return RedirectToAction("Editar", new { id = editarTagRequest.Id });


        }
        public async Task<IActionResult> Eliminar(EditarTagRequest editarTagRequest)
        {
            {

                var borrarTag = await tagRepositorio.DeleteAsync(editarTagRequest.Id);
              
                if (borrarTag != null) {

                    //mostrar noficacion de exito
                    return RedirectToAction("Listar");
                
                }
                //mostrar notificacion de error
                return RedirectToAction("Editar",new { id = editarTagRequest.Id });

            };
            //return RedirectToAction(nameof(Index));


        }
    }

}
