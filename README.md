# SibilaApp-Sena - SIBILA - Sistema de Gestión de Bibliotecas


<!--SIBILA es un sistema de gestión de bibliotecas diseñado para facilitar la administración de materiales bibliográficos y la gestión de usuarios. Permite a los bibliotecarios registrar y controlar préstamos, devoluciones, y gestionar los roles de los usuarios.
Tecnologías utilizadas

Backend: ASP.NET Core con C#

Base de datos: SQLSERVER

Frontend: HTML, CSS y Bootstrap

Herramientas de versionamiento: Git y GitHub

Instalación y configuración

Requisitos previos

.NET 6.0 o superior

MySQL Server

Visual Studio o cualquier editor compatible con C#

Git (opcional para control de versiones)
Pasos de instalación

Clonar el repositorio:

git clone https://github.com/tu-usuario/sibila.git

Navegar al directorio del proyecto:

cd sibila

Restaurar paquetes NuGet:

dotnet restore

Configurar la cadena de conexión en appsettings.json:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=sibila_db;User Id=root;Password=tu_contraseña;"
  }
}

Aplicar las migraciones de la base de datos:

dotnet ef database update

Ejecutar el proyecto:

dotnet run

Funcionalidades principales

Gestión de usuarios y roles

Registro y administración de préstamos y devoluciones

Interfaz accesible y amigable

Estructura del código

Controllers: Contiene los controladores para gestionar la lógica de usuarios, roles, prestamos y libros.

Models: Define las entidades y relaciones de la base de datos.

Views: Contiene las vistas en Razor para la interfaz de usuario.

wwwroot: Archivos estáticos como CSS, JS e imágenes.
Para cualquier consulta o sugerencia, contacta a leidistech@gmail.com -->