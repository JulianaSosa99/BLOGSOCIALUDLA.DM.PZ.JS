using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebSocialUdla.Models.ViewModels;

namespace WebSocialUdla.Controllers
{
    public class CuentaController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public CuentaController(UserManager<IdentityUser>userManager)
        {
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Registrar(RegistrarViewModel registrarViewModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = registrarViewModel.Usuario,
                Email = registrarViewModel.Email,

            };

            var identityResult = await userManager.CreateAsync(identityUser, registrarViewModel.Contrasenia);

            if (identityResult.Succeeded) 
            {
                var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");
                
                if (roleIdentityResult.Succeeded)
                { 
                    return RedirectToAction("Registrar");
                }
                
            }
            return View();
        }
    }
}
