using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using WebSocialUdla.Dominio.DTOs;
using WebSocialUdla.Services;
using WebSocialUdla.Models.ViewModels;
using WebSocialUdla.Models;
using WebSocialUdla.Servicios;
using BloggieWebProject.Models.ViewModels;
using BloggieWebProject.Models;

namespace WebSocialUdla.Controllers
{
	public class HomeController : Controller
	{
		private readonly IBlogFicaService _blogFicaService;
		private readonly IBlogNodoService _blogNodoService;
		private readonly ITagService _tagService;

		public HomeController(IBlogFicaService blogFicaService, IBlogNodoService blogNodoService, ITagService tagService)
		{
			_blogFicaService = blogFicaService;
			_blogNodoService = blogNodoService;
			_tagService = tagService;
		}

		public async Task<IActionResult> Index()
		{
			var blogPostsFica = await _blogFicaService.GetAllBlogsAsync();
			var blogPostsNodo = await _blogNodoService.GetAllBlogsAsync();
			var tags = await _tagService.GetAllTagsAsync();

			var model = new HomeViewModel
			{
				BlogPostsFica = (IEnumerable<Dominio.Models.BlogFica>)blogPostsFica,
				BlogPostsNodo = (IEnumerable<Dominio.Models.BlogNodo>)blogPostsNodo,
				Tags = (IEnumerable<Dominio.Models.Tag>)tags
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
