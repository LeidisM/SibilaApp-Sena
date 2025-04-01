using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SibilaApp.Data;
using SibilaApp.Models.Enums;
using SibilaApp.Models.ViewModels;
using SibilaApp.Models;

namespace SibilaApp.Controllers
{
    public class PrestamosController: Controller
    {
        private readonly ApplicationDbContext _context;
        public PrestamosController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var prestamo = await _context.Prestamos.Include(p => p.Usuario).Include(p => p.Libro).Where(x => x.FechaDevolucion == null).ToListAsync();
            return View(prestamo);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var prestamoVm = new PrestamoViewModel
            {
                Prestamo = new Prestamos(),

            };
            return View(prestamoVm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(PrestamoViewModel prestamoVm)
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
                return View(prestamoVm);
            }
            var libro = await _context.Libros.FirstOrDefaultAsync(l => l.ISBN == prestamoVm.ISBN);
            if (libro == null)
            {
                return View(prestamoVm);
            }

            var prestamo = new Prestamos();
            prestamo.Usuario = usuario;
            prestamo.Libro = libro;
            prestamo.FechaPrestamo = DateTime.Now;

            _context.Prestamos.Add(prestamo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //E
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
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
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
        [HttpPost]

        // ✅ Método para marcar como devuelto automáticamente
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
            }
            catch (DbUpdateConcurrencyException)
            {
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
