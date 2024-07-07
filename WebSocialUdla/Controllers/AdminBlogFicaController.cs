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
	public class AdminBlogFicaController : Controller
	{
		private readonly IBlogFicaService _blogFicaService;
		private readonly ITagService _tagService;

		public AdminBlogFicaController(IBlogFicaService blogFicaService, ITagService tagService)
		{
			_blogFicaService = blogFicaService;
			_tagService = tagService;
		}

		[HttpGet]
		public async Task<IActionResult> Agregar()
		{
			var tags = await _tagService.GetAllTagsAsync();

			var model = new BlogFicaDto
			{
				Tags = (ICollection<TagDto>)tags.Select(x => new SelectListItem { Text = x.Nombre, Value = x.Id.ToString() })
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Agregar(BlogFicaDto blogFicaDto)
		{
			if (ModelState.IsValid)
			{
				await _blogFicaService.CreateBlogAsync(blogFicaDto);
				return RedirectToAction("Lista");
			}

			var tags = await _tagService.GetAllTagsAsync();
			blogFicaDto.Tags = (ICollection<TagDto>)tags.Select(x => new SelectListItem { Text = x.Nombre, Value = x.Id.ToString() });

			return View(blogFicaDto);
		}

		[HttpGet]
		public async Task<IActionResult> Lista()
		{
			var blogs = await _blogFicaService.GetAllBlogsAsync();
			return View(blogs);
		}

		[HttpGet]
		public async Task<IActionResult> Editar(Guid id)
		{
			var blog = await _blogFicaService.GetBlogAsync(id);
			var tags = await _tagService.GetAllTagsAsync();

			if (blog != null)
			{
				var model = new BlogFicaDto
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
					Tags = (ICollection<TagDto>)tags.Select(x => new SelectListItem { Text = x.Nombre, Value = x.Id.ToString() }),
					TagSeleccionado = blog.Tags.Select(x => x.Id.ToString()).ToArray()
				};
				return View(model);
			}

			return View(null);
		}

		[HttpPost]
		public async Task<IActionResult> Editar(BlogFicaDto blogFicaDto)
		{
			if (ModelState.IsValid)
			{
				var blogActualizado = await _blogFicaService.UpdateBlogAsync(blogFicaDto.Id, blogFicaDto);
				if (blogActualizado != null)
				{
					return RedirectToAction("Lista");
				}
			}

			var tags = await _tagService.GetAllTagsAsync();
			blogFicaDto.Tags = (ICollection<TagDto>)tags.Select(x => new SelectListItem { Text = x.Nombre, Value = x.Id.ToString() });

			return View(blogFicaDto);
		}

		[HttpPost]
		public async Task<IActionResult> Eliminar(Guid id)
		{
			var blogEliminado = await _blogFicaService.DeleteBlogAsync(id);

			if (blogEliminado)
			{
				return RedirectToAction("Lista");
			}
			return RedirectToAction("Editar", new { id });
		}
	}
}
