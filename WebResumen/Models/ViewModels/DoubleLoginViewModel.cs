﻿using System.ComponentModel.DataAnnotations;

namespace WebResumen.Models.ViewModels
{
    public class DoubleLoginViewModel
    {
        [Required(ErrorMessage = "Usuario es requerido")]
        [StringLength(100, ErrorMessage = "Nombre de usuario no puede estar vacio", MinimumLength = 2)]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Contraseña es requerida")]
        //[StringLength(10, ErrorMessage = "Debe tener entre 5 y 10 caracteres", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

        [Required(ErrorMessage = "Motivo es requerido")]
        [StringLength(100, ErrorMessage = "Debe tener entre 5 y 30 caracteres", MinimumLength = 5)]
        public string Comentario { get; set; }

        public string Dato { get; set; }
        public string ImpresoraSabiUno { get; set; }
    }
}
