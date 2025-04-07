using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SibilaApp.Models;

namespace SibilaApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Constructor del controlador que recibe un objeto ILogger para registrar logs.
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        // Acción que devuelve la vista de la página de inicio.
        public IActionResult Index()
        {
            return View();// Retorna la vista asociada a la acción Index.
        }

        public IActionResult Privacy()
        {
            return View(); // Acción que devuelve la vista de la página de privacidad.
        }
        // Acción para manejar errores
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
