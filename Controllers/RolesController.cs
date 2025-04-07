using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SibilaApp.Data;
using SibilaApp.Models;

namespace SibilaApp.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción que muestra el formulario inicio vista nuevo rol 
        public async Task<IActionResult> Index()
        {
            return View(await _context.Roles.ToListAsync());
        }
        // Acción GET que muestra el formulario de detalles del rol
        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roles == null)
            {
                return NotFound();
            }

            return View(roles);
        }
        // Acción GET que muestra el formulario para crear un nuevo rol
        // GET: Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Roles roles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roles);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles.FindAsync(id);
            if (roles == null)
            {
                return NotFound();
            }
            return View(roles);
        }

        // POST: Roles/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Roles roles)
        {
            if (id != roles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolesExists(roles.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(roles);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roles == null)
            {
                return NotFound();
            }

            return View(roles);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roles = await _context.Roles.FindAsync(id);
            if (roles != null)
            {
                _context.Roles.Remove(roles);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RolesExists(int id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
