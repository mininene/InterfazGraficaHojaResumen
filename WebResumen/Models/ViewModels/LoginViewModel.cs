using System.ComponentModel.DataAnnotations;

namespace WebResumen.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Usuario es requerido")]
        [StringLength(100, ErrorMessage = "Nombre de usuario no puede estar vacio", MinimumLength = 2)]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Contraseña es requerida")]
        //[StringLength(10, ErrorMessage = "Debe tener entre 5 y 10 caracteres", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }



    }
}
