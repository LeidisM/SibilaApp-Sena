using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SibilaApp.Data;
using SibilaApp.Models.Enums;
using SibilaApp.Models.ViewModels;
using SibilaApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace SibilaApp.Controllers
{
    [Authorize]
    public class DevolucionesController: Controller
    {
        private readonly ApplicationDbContext _context;
        public DevolucionesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var devoluciones = await _context.Prestamos.Include(p => p.Usuario).Include(p => p.Libro).Where(x => x.FechaDevolucion != null).ToListAsync();
            return View(devoluciones);
        }
    }
}
