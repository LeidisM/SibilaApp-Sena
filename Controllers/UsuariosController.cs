using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SibilaApp.Data;
using SibilaApp.Models;
using SibilaApp.Models.Enums;
using SibilaApp.Models.ViewModels;

namespace SibilaApp.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Acción GET que devuelve la lista de usuarios.
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }
        // Acción GET que muestra el formulario para crear un nuevo usuario.
        [HttpGet]
        public IActionResult Create()
        {
            var usuarioVm = new UsuarioViewModel
            {
                Usuario = new Models.Usuarios(),
                Roles = _context.Roles.ToList(),
                EstadoUsuarioList = Enum.GetValues(typeof(TipoDocumentoUsuarioEnum))
                          .Cast<TipoDocumentoUsuarioEnum>()
                          .Select(e => new SelectListItem
                          {
                              Value = e.ToString(),
                              Text = e.ToString()
                          }).ToList()
            };
            return View(usuarioVm);
        }
        // Acción POST que guarda un nuevo usuario en la base de datos.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Models.Usuarios usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        // Acción GET que muestra el formulario para editar un usuario existente.
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var usuarioVm = new UsuarioViewModel
            {
                Usuario = await _context.Usuarios.FindAsync(id),
                Roles = _context.Roles.ToList(),
                EstadoUsuarioList = Enum.GetValues(typeof(TipoDocumentoUsuarioEnum))
                         .Cast<TipoDocumentoUsuarioEnum>()
                         .Select(e => new SelectListItem
                         {
                             Value = e.ToString(),
                             Text = e.ToString()
                         }).ToList()
            };

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuarioVm);
        }
        // Acción POST que actualiza la información de un usuario en la base de datos.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Usuarios usuario)
        {
           
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
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

        // Acción POST que elimina un usuario de la base de datos.
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }//confirmar eliminar
        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Acción GET que muestra los detalles de un usuario específico.
        // GET: Details/Usuarios
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

    }
}
