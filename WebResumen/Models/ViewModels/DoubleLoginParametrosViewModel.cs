using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebResumen.Models.ViewModels
{
    public class DoubleLoginParametrosViewModel
    {
        [Required(ErrorMessage = "Usuario es requerido")]
        [StringLength(100, ErrorMessage = "Nombre de usuario no puede estar vacio", MinimumLength = 2)]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Contraseña es requerida")]
        //[StringLength(20, ErrorMessage = "Debe tener entre 5 y 10 caracteres", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

        [Required(ErrorMessage = "Motivo es requerido")]
        [StringLength(30, ErrorMessage = "Debe tener entre 5 y 30 caracteres", MinimumLength = 5)]
        public string Comentario { get; set; }

        public string Id { get; set; }
        public string ImpresoraSabiUno { get; set; }
        public string ImpresoraSabiDos { get; set; }
        public string RutaLog { get; set; }
        public int Tiempo { get; set; }
        public bool Reinicio { get; set; }
    }
}
