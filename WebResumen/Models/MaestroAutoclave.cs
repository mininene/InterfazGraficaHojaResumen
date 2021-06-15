using System.ComponentModel.DataAnnotations;

namespace WebResumen.Models
{
    public partial class MaestroAutoclave
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Matricula es requerida")]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "Nombre es requerido")]
        public string Nombre { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo se aceptan números")]
        [Required(ErrorMessage = "Version es requerida")]
        public int Version { get; set; }

        [RegularExpression("^(((25[0-5])|(2[0-4]\\d)|(1\\d{2})|(\\d{1,2}))\\.){3}(((25[0-5])|(2[0-4]\\d)|(1\\d{2})|(\\d{1,2})))", ErrorMessage = "Dirección IP inválida")]
        [Required(ErrorMessage = "Direccción IP es requerida")]
        public string Ip { get; set; }

        [RegularExpression("^[A-Za-z]*$", ErrorMessage = "Solo se aceptan letras")]
        [StringLength(7, ErrorMessage = "Debe tener 7 carácteres", MinimumLength = 5)]
        [Required(ErrorMessage = "Sección es requerida")]
        public string Seccion { get; set; }

        [Required(ErrorMessage = "Estado es requerido")]
        public bool? Estado { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo se aceptan números")]
        [Required(ErrorMessage = "Último ciclo es requerido")]
        [StringLength(5, ErrorMessage = "Debe tener 5 carácteres", MinimumLength = 5)]
        public string UltimoCiclo { get; set; }

        //[RegularExpression(@"^[a-zA-Z]:(\\\w+)*([\\]|[.][a-zA-Z]+)?$", ErrorMessage = "Formato de Ruta inválido")]
        [Required(ErrorMessage = "RutaSalida es requerida")]
        public string RutaSalida { get; set; }

        //[RegularExpression(@"^(([a-zA-Z]:|\\\\\w[ \w\.]*)(\\\w[ \w\.]*|\\%[ \w\.]+%+)+|%[ \w\.]+%(\\\w[ \w\.]*|\\%[ \w\.]+%+)*)", ErrorMessage = "Formato de ruta Invalido")]
        [Required(ErrorMessage = "RutaSalidaPdf es requerida")]
        public string RutaSalidaPdf { get; set; }
    }
}
