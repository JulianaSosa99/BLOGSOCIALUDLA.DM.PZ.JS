using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSocialUdla.Dominio.DTOs;
using WebSocialUdla.Services;
using WebSocialUdla.Servicios;

namespace WebSocialUdla.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminBlogNodoController : Controller
	{
		private readonly IBlogNodoService _blogNodoService;
		private readonly ITagService _tagService;

		public AdminBlogNodoController(IBlogNodoService blogNodoService, ITagService tagService)
		{
			_blogNodoService = blogNodoService;
			_tagService = tagService;
		}

		[HttpGet]
		public async Task<IActionResult> Agregar()
		{
			var tags = await _tagService.GetAllTagsAsync();

			var model = new BlogNodoDto
			{
				Tags = tags.Select(x => new TagDto { Nombre = x.Nombre, Id = x.Id }).ToList()
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Agregar(BlogNodoDto blogNodoDto)
		{
			if (ModelState.IsValid)
			{
				await _blogNodoService.CreateBlogAsync(blogNodoDto);
				return RedirectToAction("Lista");
			}

			var tags = await _tagService.GetAllTagsAsync();
			blogNodoDto.Tags = tags.Select(x => new TagDto { Nombre = x.Nombre, Id = x.Id }).ToList();

			return View(blogNodoDto);
		}

		[HttpGet]
		public async Task<IActionResult> Lista()
		{
			var blogs = await _blogNodoService.GetAllBlogsAsync();
			return View(blogs);
		}

		[HttpGet]
		public async Task<IActionResult> Editar(Guid id)
		{
			var blog = await _blogNodoService.GetBlogAsync(id);
			var tags = await _tagService.GetAllTagsAsync();

			if (blog != null)
			{
				var model = new BlogNodoDto
				{
					Id = blog.Id,
					Encabezado = blog.Encabezado,
					TituloPagina = blog.TituloPagina,
					Contenido = blog.Contenido,
					Autor = blog.Autor,
					DescripcionCorta = blog.DescripcionCorta,
					UrlImagenDestacada = blog.UrlImagenDestacada,
					FechaPublicacion = blog.FechaPublicacion,
					Visible = blog.Visible,
					Tags = (ICollection<TagDto>)tags.Select(x => new TagDto { Nombre = x.Nombre, Id = x.Id }),
					TagSeleccionado = blog.Tags.Select(x => x.Id.ToString()).ToArray()
				};
				return View(model);
			}

			return View(null);
		}

		[HttpPost]
		public async Task<IActionResult> Editar(BlogNodoDto blogNodoDto)
		{
			if (ModelState.IsValid)
			{
				var blogActualizado = await _blogNodoService.UpdateBlogAsync(blogNodoDto.Id, blogNodoDto);
				if (blogActualizado != null)
				{
					return RedirectToAction("Lista");
				}
			}

			var tags = await _tagService.GetAllTagsAsync();
			blogNodoDto.Tags = (ICollection<TagDto>)tags.Select(x => new TagDto { Nombre = x.Nombre, Id = x.Id });

			return View(blogNodoDto);
		}

		

		[HttpPost]
		public async Task<IActionResult> Eliminar(Guid id)
		{
			var blogEliminado = await _blogNodoService.DeleteBlogAsync(id);

			if (blogEliminado)
			{
				return RedirectToAction("Lista");
			}
			return RedirectToAction("Editar", new { id });
		}
	}
}
