using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSocialUdla.Dominio.DTOs;
using BloggieWebProject.Models.ViewModels;
using WebSocialUdla.Servicios;
using WebSocialUdla.Models.ViewModels;
using BloggieWebProject.Servicios;

namespace BloggieWebProject.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminUsersController : Controller
	{
		private readonly IUserService _userService;

		public AdminUsersController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		public async Task<IActionResult> Listar()
		{
			var users = await _userService.GetAllUsersAsync();

			var usersViewModel = new UserViewModel
			{
				Users = new List<UserDto>()
			};

			foreach (var user in users)
			{
				usersViewModel.Users.Add(new UserDto
				{
					Username = user.Username,
					CorreoElectronico = user.CorreoElectronico
				});
			}

			return View(usersViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Listar(UserViewModel request)
		{
			var userDto = new UserDto
			{
				Username = request.Usuario,
				Password = request.Contrasenia, // Asegúrate de manejar correctamente las contraseñas en tu aplicación
				Nombres = request.Nombres,
				Apellidos = request.Apellidos,
				CorreoElectronico = request.Email
			};

			var user = await _userService.CreateUserAsync(userDto);

			if (user != null)
			{
				var roles = new List<string> { "User" };

				if (request.AdminRoleCheckBox)
				{
					roles.Add("Admin");
				}

				await _userService.AssignRolesAsync(user.Id, roles); // Método ficticio, debes implementarlo en el servicio

				return RedirectToAction("Listar");
			}

			return View(request);
		}

		[HttpPost]
		public async Task<IActionResult> Eliminar(Guid id)
		{
			var result = await _userService.DeleteUserAsync(id);

			if (result)
			{
				return RedirectToAction("Listar");
			}

			return View();
		}
	}
}
