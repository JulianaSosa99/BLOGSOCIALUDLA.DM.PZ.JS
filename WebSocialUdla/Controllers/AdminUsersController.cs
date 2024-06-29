using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebSocialUdla.Models.ViewModels;
using WebSocialUdla.Repositorio;

namespace WebSocialUdla.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepositorio userRepositorio;
        private readonly UserManager<IdentityUser> userManager;

        public AdminUsersController(IUserRepositorio userRepositorio,
            UserManager<IdentityUser> userManager)
        {
            this.userRepositorio = userRepositorio;
            this.userManager = userManager;
        }

        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> Listar(UserViewModel request)
        {
            var identityUser = new IdentityUser
            {
                UserName = request.Usuario,
                Email = request.Email
            };

            var identityResult = 
                await userManager.CreateAsync(identityUser, request.Contrasenia);
            if (identityResult is not null) 
            {
                if (identityResult.Succeeded) 
                { 
                    //Asignar roles a este usuario
                    var roles = new List<string> { "User"};

                    if (request.AdminRoleCheckBox)
                    {
                        roles.Add("Admin");
                    }
                    identityResult= await userManager.AddToRolesAsync(identityUser, roles);

                    if (identityResult is not null && identityResult.Succeeded) 
                    {
                        return RedirectToAction("Listar", "AdminUsers");
                    }
                
                }
            }
            return View();
        }
    }
}
