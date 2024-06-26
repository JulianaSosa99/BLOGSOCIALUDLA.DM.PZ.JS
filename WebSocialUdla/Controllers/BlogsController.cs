using BloggieWebProject.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace BloggieWebProject.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepositorio blogPostRepositorio;

        public BlogsController(IBlogPostRepositorio blogPostRepositorio)
        {
            this.blogPostRepositorio = blogPostRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string manejadorUrl)
        {
            var blogPost = await blogPostRepositorio.GetByUrlHandleAsync(manejadorUrl);

            return View(blogPost);
        }
    }
}
