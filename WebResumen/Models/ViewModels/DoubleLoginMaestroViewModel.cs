using System.ComponentModel.DataAnnotations;

namespace WebResumen.Models.ViewModels
{
    public class DoubleLoginMaestroViewModel
    {
        [Required(ErrorMessage = "Usuario es requerido")]
        [StringLength(100, ErrorMessage = "Nombre de usuario no puede estar vacio", MinimumLength = 2)]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Contraseña es requerida")]
        //[StringLength(20, ErrorMessage = "Debe tener entre 5 y 10 caracteres", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

        [Required(ErrorMessage = "Motivo es requerido")]
        [StringLength(100, ErrorMessage = "Debe tener entre 5 y 30 caracteres", MinimumLength = 5)]
        public string Comentario { get; set; }

        public string Id { get; set; }
        public string Matricula { get; set; }
        public string Nombre { get; set; }
        public string Version { get; set; }
        public string Ip { get; set; }
        public string Seccion { get; set; }
        public bool Estado { get; set; }
        public string UltimoCiclo { get; set; }
        public string RutaSalida { get; set; }
        public string RutaSalidaPDF { get; set; }

    }
}
