using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebSocialUdla.Dominio.DTOs;
using WebSocialUdla.Servicios;

namespace WebSocialUdla.Controllers
{
	[Authorize(Roles = "Admin")] 
	public class AdminCommentController : Controller
	{
		private readonly ICommentService _commentService;

		public AdminCommentController(ICommentService commentService)
		{
			_commentService = commentService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var comments = await _commentService.GetAllCommentsAsync();
			return View(comments);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			var comment = await _commentService.GetCommentAsync(id);
			if (comment == null)
			{
				return NotFound(); 
			}
			return View(comment);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Guid id, CommentDto commentDto)
		{
			if (id != commentDto.Id)
			{
				return BadRequest(); 
			}

			var updatedComment = await _commentService.UpdateCommentAsync(id, commentDto);
			if (updatedComment == null)
			{
				return NotFound(); 
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Delete(Guid id)
		{
			var comment = await _commentService.GetCommentAsync(id);
			if (comment == null)
			{
				return NotFound(); 
			}
			return View(comment);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var result = await _commentService.DeleteCommentAsync(id);
			if (!result)
			{
				return NotFound(); 
			}
			return RedirectToAction(nameof(Index));
		}
	}
}

