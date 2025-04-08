using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SibilaApp.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using SibilaApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
namespace SibilaApp.Controllers
{
    
    public class AutenticacionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AutenticacionesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.CorreoElectronico == model.Email && (u.RolId == 2 || u.RolId == 3));

            
            if (usuario == null || model.Password != usuario.Contrasena)
            {
                TempData["ErrorMessage"] = "Usuario o credenciales incorrectos verificar.";
                return View(model);
            }
           
            // Crear claims para la autenticación
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.CorreoElectronico)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );            
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Autenticaciones");
        }

    }
}
