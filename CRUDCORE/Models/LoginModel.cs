using System.ComponentModel.DataAnnotations;

namespace CRUDCORE.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Ingresa tu usuario")]
        [Display(Name = "Usuario")]
        public string? NombreUsuario { get; set; }

        [Required(ErrorMessage = "Ingresa tu clave")]
        [DataType(DataType.Password)]
        [Display(Name = "Clave")]
        public string? Clave { get; set; }
    }

    public class UsuarioModel
    {
        public int IdUsuario { get; set; }
        public string? NombreUsuario { get; set; }
        public string? NombreCompleto { get; set; }
    }
}
