﻿using SibilaApp.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SibilaApp.Models
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public TipoDocumentoUsuarioEnum TipoDocumento { get; set; }
        public string Documento { get; set; }
        public string CorreoElectronico { get; set; }
        public string? Contrasena { get; set; }
        public int RolId { get; set; }
        [ForeignKey(nameof(RolId))]
        public Roles? Rol{ get; set; }


    }
}
