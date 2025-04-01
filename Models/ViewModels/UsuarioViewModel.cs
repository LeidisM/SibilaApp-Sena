using Microsoft.AspNetCore.Mvc.Rendering;
using SibilaApp.Models.Enums;

namespace SibilaApp.Models.ViewModels
{
    public class UsuarioViewModel
    {
        public Usuarios Usuario { get; set; }
        public List<SelectListItem> EstadoUsuarioList { get; set; }
        public ICollection<Roles> Roles { get; set; }
    }
}
