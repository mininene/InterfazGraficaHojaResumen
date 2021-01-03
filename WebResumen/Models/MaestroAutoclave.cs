using System;
using System.Collections.Generic;
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

        [Required(ErrorMessage = "Version es requerida")]
        public int Version { get; set; }

        [Required(ErrorMessage = "IP es requerida")]
        public string Ip { get; set; }

        [Required(ErrorMessage = "Sección es requerida")]
        public string Seccion { get; set; }

        [Required(ErrorMessage = "Estado es requerido")]
        public bool? Estado { get; set; }

        [Required(ErrorMessage = "Último ciclo es requerido")]
        [StringLength(5, ErrorMessage = "Debe tener 5 carácteres", MinimumLength = 5)]
        public string UltimoCiclo { get; set; }

        [Required(ErrorMessage = "RutaSalida es requerida")]
        public string RutaSalida { get; set; }

        [Required(ErrorMessage = "RutaSalidaPdf es requerida")]
        public string RutaSalidaPdf { get; set; }
    }
}
