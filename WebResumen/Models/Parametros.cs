using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebResumen.Models
{
    public partial class Parametros
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Impresora es requerida")]
        public string ImpresoraSabiUno { get; set; }

        [Required(ErrorMessage = "Impresora es requerida")]
        public string ImpresoraSabiDos { get; set; }

        [Required(ErrorMessage = "Ruta Log es requerida")]
         public string RutaLog { get; set; }

        [Required(ErrorMessage = "Tiempo es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Valor entre 1 y 10")]
        public int Tiempo { get; set; }

        [Required(ErrorMessage = "Tiempo es requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Valor entre 1 y 10")]
        public int? Tsesion { get; set; } //NUEVO
        public bool? Reinicio { get; set; }
    }
}
