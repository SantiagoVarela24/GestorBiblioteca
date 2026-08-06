using System.ComponentModel.DataAnnotations;

namespace CRUDCORE.Models
{
    public class PrestamoModel
    {
        public int IdPrestamo { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [Display(Name = "Nombre de la persona")]
        public string? NombrePersona { get; set; }

        [Display(Name = "Teléfono")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "El campo Título del libro es obligatorio")]
        [Display(Name = "Título del libro")]
        public string? TituloLibro { get; set; }

        [Display(Name = "Autor")]
        public string? Autor { get; set; }

        [Required(ErrorMessage = "La fecha de préstamo es obligatoria")]
        [Display(Name = "Fecha de préstamo")]
        [DataType(DataType.Date)]
        public DateTime FechaPrestamo { get; set; } = DateTime.Today;

        [Display(Name = "Fecha de devolución")]
        [DataType(DataType.Date)]
        public DateTime? FechaDevolucion { get; set; }

        public bool Devuelto { get; set; }
    }
}
