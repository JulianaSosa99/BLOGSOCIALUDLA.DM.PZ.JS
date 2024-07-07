using BloggieWebProject.Models.ViewModels;
using BloggieWebProject.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSocialUdla.Dominio.DTOs;
using WebSocialUdla.Servicios;

namespace BloggieWebProject.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminTagsController : Controller
	{
		private readonly ITagService _tagService;

		public AdminTagsController(ITagService tagService)
		{
			_tagService = tagService;
		}

		[HttpGet]
		public IActionResult Agregar()
		{
			return View();
		}

		[HttpPost]
		[ActionName("Agregar")]
		public async Task<IActionResult> Agregar(AgregarTagRequest agregarTagRequest)
		{
			if (ModelState.IsValid)
			{
				var tagDto = new TagDto
				{
					Nombre = agregarTagRequest.Nombre,
					DisplayNombre = agregarTagRequest.DisplayNombre
				};

				var tag = await _tagService.CreateTagAsync(tagDto);

				if (tag != null)
				{
					return RedirectToAction("Listar");
				}
			}

			return View(agregarTagRequest);
		}

		[HttpGet]
		[ActionName("Listar")]
		public async Task<IActionResult> Listar()
		{
			var tags = await _tagService.GetAllTagsAsync();
			return View(tags);
		}

		[HttpGet]
		public async Task<IActionResult> Editar(Guid id)
		{
			var tag = await _tagService.GetTagAsync(id);

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
			var tagDto = new TagDto
			{
				Id = editarTagRequest.Id,
				Nombre = editarTagRequest.Nombre,
				DisplayNombre = editarTagRequest.DisplayNombre
			};

			var tagActualizado = await _tagService.UpdateTagAsync(editarTagRequest.Id, tagDto);

			if (tagActualizado != null)
			{
				return RedirectToAction("Listar");
			}
			else
			{
				// Mostrar notificación de fallo
			}

			return RedirectToAction("Editar", new { id = editarTagRequest.Id });
		}

		[HttpPost]
		public async Task<IActionResult> Eliminar(Guid id)
		{
			var result = await _tagService.DeleteTagAsync(id);

			if (result)
			{
				// Mostrar notificación de éxito
				return RedirectToAction("Listar");
			}
			else
			{
				// Mostrar notificación de error
				return RedirectToAction("Editar", new { id });
			}
		}
	}
}
