﻿using SibilaApp.Models.Enums;

namespace SibilaApp.Models
{
    public class Libros
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string? Autor { get; set; }
        public string? Editorial { get; set; }
        public string? ISBN { get; set; }
        public string? Subcategoria { get; set; }
        public string? TipoMaterial { get; set; }
        public EstadoLibroEnum Estado { get; set; }      
    }
}
