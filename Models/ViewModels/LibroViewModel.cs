using Microsoft.AspNetCore.Mvc.Rendering;
using SibilaApp.Models.Enums;

namespace SibilaApp.Models.ViewModels
{
    public class LibroViewModel
    {
        public Libros Libro { get; set; }
        public List<SelectListItem>? EstadolibrosEnum { get; set; }

    }



}
            
        
    


