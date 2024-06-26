using BloggieWebProject.Models;
using BloggieWebProject.Models.ViewModels;
using BloggieWebProject.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BloggieWebProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepositorio blogPostRepositorio;
		private readonly ITagRepositorio tagRepositorio;

		public HomeController(ILogger<HomeController> logger, 
            IBlogPostRepositorio blogPostRepositorio,
            ITagRepositorio tagRepositorio
            )
        {
            _logger = logger;
            this.blogPostRepositorio = blogPostRepositorio;
			this.tagRepositorio = tagRepositorio;
		}

        public async Task<IActionResult> Index()
        {
            //tener todos los blogs
            var blogPosts= await blogPostRepositorio.GetAllAsync();

            //tener todos los tags
            var tags = await tagRepositorio.GetAllAsync();
            var model = new HomeViewModel
            {
                BlogPosts = blogPosts,
                Tags = tags
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
