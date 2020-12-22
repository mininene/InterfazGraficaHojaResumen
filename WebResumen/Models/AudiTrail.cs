using System;
using System.Collections.Generic;

#nullable disable

namespace WebResumen.Models
{
    public partial class AudiTrail
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public DateTime? FechaHora { get; set; }
        public string Evento { get; set; }
        public string Valor { get; set; }
        public string ValorActualizado { get; set; }
        public string Comentario { get; set; }
    }
}
