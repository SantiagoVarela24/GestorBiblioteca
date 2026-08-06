using CRUDCORE.Datos;
using CRUDCORE.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRUDCORE.Controllers
{
    public class AccountController : Controller
    {
        UsuarioDatos _UsuarioDatos = new UsuarioDatos();

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Listar", "Prestamo");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel oLogin)
        {
            if (!ModelState.IsValid)
                return View(oLogin);

            var oUsuario = _UsuarioDatos.ValidarLogin(oLogin.NombreUsuario!, oLogin.Clave!);

            if (oUsuario == null)
            {
                ViewBag.Error = "Usuario o clave incorrectos";
                return View(oLogin);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, oUsuario.NombreUsuario!),
                new Claim("NombreCompleto", oUsuario.NombreCompleto ?? oUsuario.NombreUsuario!)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                new AuthenticationProperties { IsPersistent = true });

            return RedirectToAction("Listar", "Prestamo");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
