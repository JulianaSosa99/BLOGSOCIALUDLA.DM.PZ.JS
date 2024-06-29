using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebSocialUdla.Models.ViewModels;
using WebSocialUdla.Repositorio;

namespace WebSocialUdla.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepositorio userRepositorio;

        public AdminUsersController(IUserRepositorio userRepositorio)
        {
            this.userRepositorio = userRepositorio;
        }

        public async Task<IActionResult> Listar()
        {
            var users = await userRepositorio.GetAll();
            var usersViewmodel = new UserViewModel();
            usersViewmodel.Users = new List<User>();

            foreach (var user in users) 
            {
                usersViewmodel.Users.Add(new Models.ViewModels.User
                { 
                        Id = Guid.Parse( user.Id),
                        Usuario= user.UserName,
                        Email = user.Email
                });
            }

            return View(usersViewmodel);
        }
    }
}
