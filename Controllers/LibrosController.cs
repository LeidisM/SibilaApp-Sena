using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SibilaApp.Data;
using SibilaApp.Models;
using SibilaApp.Models.Enums;
using SibilaApp.Models.ViewModels;

namespace SibilaApp.Controllers
{
    public class LibrosController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LibrosController(ApplicationDbContext context) // Contexto de base de datos para acceder a la información de los libros.
        {
            _context = context;
        }
        // Acción que devuelve la vista con la lista de libros.
        public async Task<IActionResult> Index()
        {
            return View(await _context.Libros.ToListAsync());
        }
        // Acción GET para mostrar el formulario de creación de un nuevo libro.
        [HttpGet]
        public IActionResult Create()
        {
            var libroVm = new LibroViewModel
            {
                Libro = new Libros(),
                EstadolibrosEnum = Enum.GetValues(typeof(EstadoLibroEnum))
                          .Cast<EstadoLibroEnum>()
                          .Select(e => new SelectListItem
                          {
                              Value = e.ToString(),
                              Text = e.ToString()
                          }).ToList()
            };
            return View(libroVm);
        }
        // Acción POST para guardar un nuevo libro en la base de datos.
        [HttpPost]
        public async Task<IActionResult> Create(LibroViewModel libroVM)
        {
            libroVM.EstadolibrosEnum = Enum.GetValues(typeof(EstadoLibroEnum))
                          .Cast<EstadoLibroEnum>()
                          .Select(e => new SelectListItem
                          {
                              Value = e.ToString(),
                              Text = e.ToString()
                          }).ToList();

            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    foreach (var error in ModelState[key].Errors)
                    {
                        Console.WriteLine($"Error en {key}: {error.ErrorMessage}");
                    }
                }
                return View(libroVM); // Devuelve la vista si hay errores
            }
            _context.Libros.Add(libroVM.Libro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         
        }
        // Acción GET para mostrar el formulario de edición de un libro existente.
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var libroVm = new LibroViewModel
            {
                Libro = await _context.Libros.FindAsync(id),
                EstadolibrosEnum = Enum.GetValues(typeof(EstadoLibroEnum))
                          .Cast<EstadoLibroEnum>()
                          .Select(e => new SelectListItem
                          {
                              Value = e.ToString(),
                              Text = e.ToString()
                          }).ToList()
            };

            var libros = await _context.Libros.FindAsync(id);
            if (libros == null)
            {
                return NotFound();
            }
            return View(libroVm);
        }
        // Acción POST para guardar los cambios realizados a un libro existente.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LibroViewModel libroVm)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libroVm.Libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        // Acción GET para confirmar la eliminación de un libro.
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }
        // Acción POST para eliminar un libro de la base de datos.
        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro != null)
            {
                _context.Libros.Remove(libro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Acción GET para mostrar los detalles de un libro.
        // GET: Details/Usuarios
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

    }
}
    
   
