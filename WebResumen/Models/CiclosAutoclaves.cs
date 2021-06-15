using System;

namespace WebResumen.Models
{
    public partial class CiclosAutoclaves
    {
        public int Id { get; set; }
        public string IdAutoclave { get; set; }
        public string IdSeccion { get; set; }
        public string Tinicio { get; set; }
        public string NumeroCiclo { get; set; }
        public string Programa { get; set; }
        public string Modelo { get; set; }
        public string Programador { get; set; }
        public string Operador { get; set; }
        public string CodigoProducto { get; set; }
        public string Lote { get; set; }
        public string Notas { get; set; }
        public string IdUsuario { get; set; }
        public string Fase1 { get; set; }
        public string DuracionTotalF1 { get; set; }
        public string Fase2 { get; set; }
        public string DuracionTotalF2 { get; set; }
        public string Fase3 { get; set; }
        public string DuracionTotalF3 { get; set; }
        public string Fase4 { get; set; }
        public string DuracionTotalF4 { get; set; }
        public string Fase5 { get; set; }
        public string DuracionTotalF5 { get; set; }
        public string Tif5 { get; set; }
        public string TisubF5 { get; set; }
        public string Tff5 { get; set; }
        public string TfsubF5 { get; set; }
        public string Fase6 { get; set; }
        public string DuracionTotalF6 { get; set; }
        public string Tif6 { get; set; }
        public string TisubF6 { get; set; }
        public string Tff6 { get; set; }
        public string TfsubF6 { get; set; }
        public string Fase7 { get; set; }
        public string DuracionTotalF7 { get; set; }
        public string Tif7 { get; set; }
        public string TisubF7 { get; set; }
        public string Fase8 { get; set; }
        public string DuracionTotalF8 { get; set; }
        public string Tif8 { get; set; }
        public string TisubF8 { get; set; }
        public string Tff8 { get; set; }
        public string Fase9 { get; set; }
        public string DuracionTotalF9 { get; set; }
        public string Tif9 { get; set; }
        public string TisubF9 { get; set; }
        public string Tff9 { get; set; }
        public string Fase10 { get; set; }
        public string DuracionTotalF10 { get; set; }
        public string Fase11 { get; set; }
        public string DuracionTotalF11 { get; set; }
        public string Fase12 { get; set; }
        public string DuracionTotalF12 { get; set; }
        public string Fase13 { get; set; }
        public string Tff13 { get; set; }
        public string TfsubF13 { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string EsterilizacionN { get; set; }
        public string Tminima { get; set; }
        public string Tmaxima { get; set; }
        public string DuracionTotal { get; set; }
        public string FtzMin { get; set; }
        public string FtzMax { get; set; }
        public string DifMaxMin { get; set; }
        public string AperturaPuerta { get; set; }
        public string TiempoCiclo { get; set; }
        public string ErrorCiclo { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
