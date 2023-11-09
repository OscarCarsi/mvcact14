using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcFei.Models
{
    public class AlumnoCarreraViewModel
    {
        public List<Alumno> Alumnos { get; set; }
        public SelectList Carreras { get; set; }
        public string? AlumnoCarrera { get; set;}
        public string? SearchString { get; set; }
    }
}
