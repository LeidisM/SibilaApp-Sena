using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SibilaApp.Models;

namespace SibilaApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Constructor del controlador que recibe un objeto ILogger para registrar logs.
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        // Acci�n que devuelve la vista de la p�gina de inicio.
        public IActionResult Index()
        {
            return View();// Retorna la vista asociada a la acci�n Index.
        }

        public IActionResult Privacy()
        {
            return View(); // Acci�n que devuelve la vista de la p�gina de privacidad.
        }
        // Acci�n para manejar errores con configuraci�n de cach� deshabilitada.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
