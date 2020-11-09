using System;
using System.Collections.Generic;

namespace WebResumen.Models
{
    public partial class MaestroAutoclave
    {
        public int Id { get; set; }
        public string Matricula { get; set; }
        public string Nombre { get; set; }
        public int Version { get; set; }
        public string Ip { get; set; }
        public string Seccion { get; set; }
        public bool? Estado { get; set; }
        public string UltimoCiclo { get; set; }
        public string RutaSalida { get; set; }
    }
}
