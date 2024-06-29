using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebSocialUdla.Models.ViewModels;

namespace WebSocialUdla.Controllers
{
    public class CuentaController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public CuentaController(UserManager<IdentityUser>userManager,
            SignInManager<IdentityUser>signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewmodel loginViewmodel)
        {
            var signInResult = await signInManager.PasswordSignInAsync(loginViewmodel.Usuario,
                loginViewmodel.Contrasenia, false, false);
            if (signInResult != null & signInResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            //Mostrar errores
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    
    }
}
