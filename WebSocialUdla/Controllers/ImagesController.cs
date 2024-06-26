using BloggieWebProject.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BloggieWebProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepositorio imageRepositorio;

        public ImagesController(IImageRepositorio imageRepositorio)
        {
            this.imageRepositorio = imageRepositorio;
        }
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    return Ok("GET Images API llamada");
        //}

        [HttpPost]
        public  async Task<IActionResult> UpdloadAsync(IFormFile file)
        {
            //call a repository
            var imageURL = await imageRepositorio.UploadAsync(file);
            if(imageURL == null)
            {
                return Problem("Ups, algo salio mal!", null, (int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new { link = imageURL });
        }
    }
}
