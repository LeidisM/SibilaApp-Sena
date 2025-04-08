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
    public class PrestamosController: Controller
    {
        private readonly ApplicationDbContext _context;
        public PrestamosController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Acción GET para obtener la lista de préstamos activos (sin devolución).
        public async Task<IActionResult> Index()
        {
            var prestamo = await _context.Prestamos.Include(p => p.Usuario).Include(p => p.Libro).Where(x => x.FechaDevolucion == null).ToListAsync();
            return View(prestamo);
        }
        // Acción GET para mostrar el formulario de creación de un préstamo.
        [HttpGet]
        public IActionResult Create()
        {
            var prestamoVm = new PrestamoViewModel
            {
                Prestamo = new Prestamos(),

            };
            return View(prestamoVm);
        }
        // Acción POST para registrar un nuevo préstamo en la base de datos.
        [HttpPost]
        public async Task<IActionResult> Create(PrestamoViewModel prestamoVm)
        {
            // Validaciones básicas para asegurarse de que los campos requeridos no están vacíos.
            if (string.IsNullOrEmpty(prestamoVm.Documento))
            {
                return View(prestamoVm);
            }
            if (string.IsNullOrEmpty(prestamoVm.ISBN))
            {
                return View(prestamoVm);
            }
            // Busca al usuario en la base de datos según su documento.
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Documento == prestamoVm.Documento);
            if (usuario == null)
            {
                TempData["ErrorMessage"] = "Usuario no encontrado.";
                return View(prestamoVm);
            }
           
            // Busca el libro en la base de datos según su ISBN.
            var libro = await _context.Libros.FirstOrDefaultAsync(l => l.ISBN == prestamoVm.ISBN);
            if (libro == null)
            {                
                TempData["ErrorMessage"] = "Libro no encontrado.";
                return View(prestamoVm);
            }
            // Crea un nuevo préstamo con la fecha actual.
            var prestamo = new Prestamos();
            prestamo.Usuario = usuario;
            prestamo.Libro = libro;
            prestamo.FechaPrestamo = DateTime.Now;

            _context.Prestamos.Add(prestamo);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Préstamo registrado correctamente.";
            return RedirectToAction(nameof(Index));
        }
        // Acción GET para mostrar el formulario de edición de un préstamo existente.
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var prestamo = await _context.Prestamos.Include(p => p.Usuario).Include(p => p.Libro).Where(x => x.Id == id).ToListAsync();
            
            if (prestamo == null)
            {
                return NotFound();
            }
            var prestamoVm = new PrestamoViewModel
            {
                Prestamo = prestamo.FirstOrDefault(),
                Documento = prestamo.FirstOrDefault().Usuario.Documento,
                ISBN = prestamo.FirstOrDefault().Libro.ISBN
            }; 

            return View(prestamoVm);
        }
        // Acción POST para actualizar la información de un préstamo.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PrestamoViewModel prestamoVm)
        {
            if (string.IsNullOrEmpty(prestamoVm.Documento))
            {
                return View(prestamoVm);
            }
            if (string.IsNullOrEmpty(prestamoVm.ISBN))
            {
                return View(prestamoVm);
            }
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Documento == prestamoVm.Documento);
            if (usuario == null)
            {
                TempData["ErrorMessage"] = "Usuario o libro no encontrado.";
                return View(prestamoVm);
            }
            var libro = await _context.Libros.FirstOrDefaultAsync(l => l.ISBN == prestamoVm.ISBN);
            if (libro == null)
            {
                return View(prestamoVm);
            }

            prestamoVm.Prestamo.Usuario = usuario;
            prestamoVm.Prestamo.Libro = libro;
            //prestamoVm.Prestamo.FechaPrestamo = DateTime.Now;

            try
            {
                _context.Update(prestamoVm.Prestamo);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Préstamo actualizado correctamente.";
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        // Acción GET para mostrar los detalles de un préstamo.
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var prestamo = await _context.Prestamos.Include(p => p.Usuario).Include(p => p.Libro).FirstOrDefaultAsync(x => x.Id == id);

            //.FirstOrDefaultAsync(m => m.Id == id);
            if (prestamo == null)
            {
                return NotFound();
            }
            
            return View(prestamo);
        }

        // Acción GET para devolver un libro, asignando la fecha de devolución.
        // Método para marcar como devuelto automáticamente
        [HttpPost]
        [HttpGet]
        public async Task<IActionResult> Devolver(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }

            // Asignar la fecha actual como fecha de devolución
            prestamo.FechaDevolucion = DateTime.Now;

            try
            {
                _context.Update(prestamo);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Libro devuelto correctamente.";
            }
            catch (DbUpdateConcurrencyException)
            {
                TempData["ErrorMessage"] = "Error al devolver el libro.";
                throw;
            }

            return RedirectToAction(nameof(Index)); // Redirige a la lista de préstamos
        }
        public async Task<IActionResult> Devoluciones()
        {
            var devoluciones = await _context.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.Libro)
                .Where(p => p.FechaDevolucion != null) // Filtra solo los préstamos devueltos
                .ToListAsync();

            return View(devoluciones);
        }


    }
}
