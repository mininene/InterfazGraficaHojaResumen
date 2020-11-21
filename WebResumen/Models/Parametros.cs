using System;
using System.Collections.Generic;

namespace WebResumen.Models
{
    public partial class Parametros
    {
        public int Id { get; set; }
        public string ImpresoraSabiUno { get; set; }
        public string ImpresoraSabiDos { get; set; }
        public string RutaLog { get; set; }
        public int Tiempo { get; set; }
        public bool? Reinicio { get; set; }
    }
}
