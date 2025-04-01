using System.ComponentModel.DataAnnotations.Schema;

namespace SibilaApp.Models
{
    public class Prestamos
    {
        public int Id { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public int UsuarioId { get; set; }
        [ForeignKey(nameof(UsuarioId))]
        public Usuarios Usuario { get; set; }
        public int LibroId { get; set; }
        [ForeignKey(nameof(LibroId))]
        public Libros Libro { get; set; }
    }
}
