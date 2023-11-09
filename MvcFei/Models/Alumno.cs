using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcFei.Models
{
    public class Alumno
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Matrícula")]

        public string AlumnoId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El campo {0} debe ser una cadena con un mínimo de {1} y un máximo de {2} caraceres. ")]

        public string Nombre { get; set; }

        [DisplayName("Fecha de ingreso")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Date, ErrorMessage = "El campo {0} debe ser una fecha")]

        public DateTime FechaIngreso { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(8, ErrorMessage = "El campo {0} debe ser una cadena con un mínimo de {1} y un máximo de {2} caraceres. ")]

        public string Carrera { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(1, 10, ErrorMessage = "El campo {0} debe ser una cadena con un mínimo de {1} y un máximo de {2} caraceres. ")]
        [Column(TypeName ="decimal(18,2)")]

        public decimal Promedio { get; set; }
    }
}
