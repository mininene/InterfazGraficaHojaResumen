using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using WebResumen.Models;

namespace WebResumen.Services.printerServiceAS
{
    public class PrinterDosTresCuatroAS : IPrinterDosTresCuatroAS
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public PrinterDosTresCuatroAS(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public void printDosTresCuatroAS(int? id)
        {
            var q = _context.CiclosAutoclaves.FirstOrDefault(m => m.Id == id);


            using (var pdf = new PdfDocument())
            {

                pdf.Info.Title = "PDF";
                PdfPage pdfPage = pdf.AddPage();
                XGraphics graph = XGraphics.FromPdfPage(pdfPage);
                XFont font = new XFont("Verdana", 8, XFontStyle.Regular);
                XFont fontDos = new XFont("Verdana", 7, XFontStyle.Regular);
                XFont negrita = new XFont("Verdana", 8, XFontStyle.Bold);
                XFont negritaDos = new XFont("Verdana", 7, XFontStyle.Bold);

                XTextFormatter tf = new XTextFormatter(graph);
                XRect rect = new XRect(40, 100, 250, 220);
                XRect recta = new XRect(130, 105, 430, 20); //25/06/2021
                //graph.DrawRectangle(XBrushes.SeaShell, rect);


                rect = new XRect(340, 540, 250, 220);
                recta = new XRect(130, 105, 430, 20); //25/06/2021
                graph.DrawRectangle(XBrushes.White, rect);
                tf.Alignment = XParagraphAlignment.Justify;
                // tf.DrawString(q.ErrorCiclo, font, XBrushes.Black, rect, XStringFormats.TopLeft);

                IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();

                graph.DrawString("ID. MAQUINA:" + "  " + q.IdAutoclave, font, XBrushes.Black, new XRect(20, 5, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("N.PROGRESIVO:" + "  " + q.NumeroCiclo, font, XBrushes.Black, new XRect(140, 5, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("Informe de ciclo de esterilización", font, XBrushes.Black, new XRect(270, 5, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("Impreso: " + DateTime.Now, font, XBrushes.Black, new XRect(440, 5, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("Por: " + _httpContextAccessor.HttpContext.Session.GetString("SessionName"), font, XBrushes.Black, new XRect(440, 15, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("PROGRAMA:", font, XBrushes.Black, new XRect(20, 25, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.Programa + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(130, 25, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.IdSeccion, font, XBrushes.Black, new XRect(20, 35, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("PROGRAMADOR:", font, XBrushes.Black, new XRect(20, 55, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.Programador, font, XBrushes.Black, new XRect(130, 55, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("OPERADOR:", font, XBrushes.Black, new XRect(20, 65, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.Operador, font, XBrushes.Black, new XRect(130, 65, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("CODIGO PRODUCTO:", font, XBrushes.Black, new XRect(20, 75, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.CodigoProducto + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(130, 75, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("N.LOTE:", font, XBrushes.Black, new XRect(20, 85, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.Lote + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(130, 85, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("ID. MAQUINA:", font, XBrushes.Black, new XRect(20, 95, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.IdAutoclave + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(130, 95, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("NOTAS:", font, XBrushes.Black, new XRect(20, 105, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
               // graph.DrawString(q.Notas.Trim() + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(130, 105, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                tf.DrawString(q.Notas.Trim() + " " + "<--[  ]", negritaDos, XBrushes.Black, recta, XStringFormats.TopLeft); //cambio 25/06/2021


                graph.DrawString("MODELO:", font, XBrushes.Black, new XRect(20, 125, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.Modelo, font, XBrushes.Black, new XRect(130, 125, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("N.PROGRESIVO:", font, XBrushes.Black, new XRect(20, 135, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.NumeroCiclo + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(130, 135, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("FASE 1:  " + q.Fase1, font, XBrushes.Black, new XRect(20, 155, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 155, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF1 + " " + "min.s", font, XBrushes.Black, new XRect(320, 155, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("FASE 2:  " + q.Fase2, font, XBrushes.Black, new XRect(20, 170, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 170, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF2 + " " + "min.s", font, XBrushes.Black, new XRect(320, 170, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                graph.DrawString("FASE 3:  " + q.Fase3, font, XBrushes.Black, new XRect(20, 185, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 185, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF3 + " " + "min.s", font, XBrushes.Black, new XRect(320, 185, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("FASE 4:  " + q.Fase4, font, XBrushes.Black, new XRect(20, 200, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 200, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF4 + " " + "min.s", font, XBrushes.Black, new XRect(320, 200, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);



                graph.DrawString("FASE 5:  " + q.Fase5, font, XBrushes.Black, new XRect(20, 220, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 220, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF5, negrita, XBrushes.Black, new XRect(320, 220, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("min.s", font, XBrushes.Black, new XRect(350, 220, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("<--[  ]", negrita, XBrushes.Black, new XRect(375, 220, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                var t = q.Tinicio.Split(" ");
                var fechaInicio = t[0];
                var HoraInicio = t[2];
                DateTime temp = DateTime.ParseExact(fechaInicio, "dd/MM/yy", CultureInfo.InvariantCulture);
                DateTime fechaInicioFormat = DateTime.ParseExact(temp.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (HoraInicio.Contains("."))
                {
                    var h = HoraInicio.Split(".");
                    var dia = Convert.ToInt32(h[0]);
                    var hora = h[1];

                    DateTime fecha = fechaInicioFormat.AddDays(dia);
                    graph.DrawString("I " + " " + fecha.ToShortDateString() + " " + hora, negrita, XBrushes.Black, new XRect(460, 220, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                }
                else
                {
                    // graph.DrawString("I " + " " + q.Tinicio, negrita, XBrushes.Black, new XRect(460, 220, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("I " + " " + fechaInicioFormat.ToShortDateString() + " " + HoraInicio, negrita, XBrushes.Black, new XRect(460, 220, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                }

                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 235, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.Tif5, font, XBrushes.Black, new XRect(230, 235, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.TisubF5, font, XBrushes.Black, new XRect(460, 235, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10 ", font, XBrushes.Black, new XRect(20, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                //graph.DrawString(q.TFF5, font, XBrushes.Black, new XRect(230, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                var tff5 = q.Tff5.Split(new String[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                graph.DrawString(tff5[0].Trim(), font, XBrushes.Black, new XRect(230, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(tff5[1].Trim(), negrita, XBrushes.Black, new XRect(263, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString($"{tff5[2]}  {tff5[3]}  {tff5[4]}  {tff5[5]}  {tff5[6]}", font, XBrushes.Black, new XRect(290, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.TfsubF5, font, XBrushes.Black, new XRect(460, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                //graph.DrawString(q.Tff5.Substring(0, 6), font, XBrushes.Black, new XRect(230, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                //graph.DrawString(q.Tff5.Substring(6, 6), negrita, XBrushes.Black, new XRect(255, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                //graph.DrawString(q.Tff5.Substring(11), font, XBrushes.Black, new XRect(275, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                //graph.DrawString(q.TfsubF5, font, XBrushes.Black, new XRect(460, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                graph.DrawString("FASE 6:  " + q.Fase6, font, XBrushes.Black, new XRect(20, 270, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 270, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF6, negrita, XBrushes.Black, new XRect(320, 270, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("min.s", font, XBrushes.Black, new XRect(350, 270, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("<--[  ]", negrita, XBrushes.Black, new XRect(375, 270, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 285, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                var tif6 = q.Tif6.Split(new String[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                graph.DrawString(tif6[0].Trim(), font, XBrushes.Black, new XRect(230, 285, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(tif6[1].Trim(), negrita, XBrushes.Black, new XRect(263, 285, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString($"{tif6[2]}  {tif6[3]}  {tif6[4]}  {tif6[5]}  {tif6[6]}", font, XBrushes.Black, new XRect(290, 285, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.TisubF6, font, XBrushes.Black, new XRect(460, 285, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                //graph.DrawString(q.TIF6, font, XBrushes.Black, new XRect(230, 285, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                //graph.DrawString(q.Tif6.Substring(0, 6), font, XBrushes.Black, new XRect(230, 285, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                //graph.DrawString(q.Tif6.Substring(6, 6), negrita, XBrushes.Black, new XRect(255, 285, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                //graph.DrawString(q.Tif6.Substring(11), font, XBrushes.Black, new XRect(275, 285, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                //graph.DrawString(q.TisubF6, font, XBrushes.Black, new XRect(460, 285, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 300, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                var tff6 = q.Tff6.Split(new String[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                graph.DrawString(tff6[0].Trim(), font, XBrushes.Black, new XRect(230, 300, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(tff6[1].Trim(), negrita, XBrushes.Black, new XRect(263, 300, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString($"{tff6[2]}  {tff6[3]}  {tff6[4]}  {tff6[5]}  {tff6[6]}", font, XBrushes.Black, new XRect(290, 300, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.TfsubF6.Substring(0, 2), font, XBrushes.Black, new XRect(460, 300, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.TfsubF6.Substring(2) + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(465, 300, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                //graph.DrawString(q.Tff6.Substring(0, 6), font, XBrushes.Black, new XRect(230, 300, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                //graph.DrawString(q.Tff6.Substring(6, 6), negrita, XBrushes.Black, new XRect(255, 300, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                //graph.DrawString(q.Tff6.Substring(11), font, XBrushes.Black, new XRect(275, 300, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                //graph.DrawString(q.TfsubF6.Substring(0, 2), font, XBrushes.Black, new XRect(460, 300, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                //graph.DrawString(q.TfsubF6.Substring(2) + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(465, 300, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);



                graph.DrawString("FASE 7:  " + q.Fase7, font, XBrushes.Black, new XRect(20, 320, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 320, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF7, negrita, XBrushes.Black, new XRect(320, 320, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("min.s", font, XBrushes.Black, new XRect(350, 320, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("<--[  ]", negrita, XBrushes.Black, new XRect(375, 320, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 335, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                var tif7 = q.Tif7.Split(new String[] { "  " }, StringSplitOptions.RemoveEmptyEntries);

                graph.DrawString(tif7[0].Trim(), font, XBrushes.Black, new XRect(230, 335, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(tif7[1].Trim(), negrita, XBrushes.Black, new XRect(263, 335, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString($"{tif7[2]}  {tif7[3]}  {tif7[4]}  {tif7[5]}  {tif7[6]}", font, XBrushes.Black, new XRect(290, 335, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.TisubF7, font, XBrushes.Black, new XRect(460, 335, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);




                graph.DrawString("FASE 8:  " + q.Fase8, font, XBrushes.Black, new XRect(20, 355, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 355, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF8 + " " + "min.s", font, XBrushes.Black, new XRect(320, 355, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 370, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.Tif8, font, XBrushes.Black, new XRect(230, 370, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.TisubF8, font, XBrushes.Black, new XRect(460, 370, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DATOS FINALES DE FASE:", font, XBrushes.Black, new XRect(20, 385, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.Tff8, font, XBrushes.Black, new XRect(230, 385, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                graph.DrawString("FASE 9:  " + q.Fase9, font, XBrushes.Black, new XRect(20, 400, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 400, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF9 + " " + "min.s", font, XBrushes.Black, new XRect(320, 400, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 415, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.Tif9, font, XBrushes.Black, new XRect(230, 415, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.TisubF9, font, XBrushes.Black, new XRect(460, 415, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("DATOS FINALES DE FASE:", font, XBrushes.Black, new XRect(20, 430, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.Tff9, font, XBrushes.Black, new XRect(230, 430, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                graph.DrawString("FASE 10: " + q.Fase10, font, XBrushes.Black, new XRect(20, 445, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 445, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF10 + " " + "min.s", font, XBrushes.Black, new XRect(320, 445, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                graph.DrawString("FASE 11: " + q.Fase11, font, XBrushes.Black, new XRect(20, 460, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 460, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF11 + " " + "min.s", font, XBrushes.Black, new XRect(320, 460, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                graph.DrawString("FASE 12: " + q.Fase12, font, XBrushes.Black, new XRect(20, 475, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 475, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF12 + " " + "min.s", font, XBrushes.Black, new XRect(320, 475, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                graph.DrawString("FASE 13: FIN DE CICLO         TIEMPO TPO-C AG. PRES. TE2 TE3 TE4 TE9 TE10", font, XBrushes.Black, new XRect(20, 495, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                var tff13 = q.Tff13.Split(new String[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                graph.DrawString(tff13[0], font, XBrushes.Black, new XRect(20, 510, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.TiempoCiclo, font, XBrushes.Black, new XRect(80, 510, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString($"{tff13[1]}  {tff13[2]}  {tff13[3]}  {tff13[4]}  {tff13[5]}  {tff13[6]}", font, XBrushes.Black, new XRect(160, 510, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                //graph.DrawString(q.Tff13.Substring(0, 6), font, XBrushes.Black, new XRect(20, 510, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                //graph.DrawString(q.TiempoCiclo, font, XBrushes.Black, new XRect(80, 510, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                //graph.DrawString(q.Tff13.Substring(6), font, XBrushes.Black, new XRect(160, 510, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.TfsubF13.Substring(0, 2), font, XBrushes.Black, new XRect(460, 510, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.TfsubF13.Substring(2), font, XBrushes.Black, new XRect(465, 510, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);




                graph.DrawString("HORA COMIEN.PROGR :", font, XBrushes.Black, new XRect(20, 530, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.HoraInicio, negrita, XBrushes.Black, new XRect(160, 530, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("HORA FIN.PROGR :", font, XBrushes.Black, new XRect(20, 545, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.HoraFin, negrita, XBrushes.Black, new XRect(160, 545, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

              

                if (q.EsterilizacionN == "")
                {
                    graph.DrawString("ESTERILIZACION:", font, XBrushes.Black, new XRect(20, 560, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FALLIDA", font, XBrushes.Black, new XRect(160, 560, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                }
                else
                {
                    graph.DrawString("ESTERILIZACION N.:", font, XBrushes.Black, new XRect(20, 560, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(q.EsterilizacionN, font, XBrushes.Black, new XRect(160, 560, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                }

                graph.DrawString("TEMP.MIN.ESTERILIZACION:", font, XBrushes.Black, new XRect(20, 575, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("°C  ", font, XBrushes.Black, new XRect(160, 575, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.Tminima + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(175, 575, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("TEMP.MAX.ESTERILIZACION:", font, XBrushes.Black, new XRect(20, 590, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("°C  ", font, XBrushes.Black, new XRect(160, 590, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.Tmaxima + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(175, 590, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                graph.DrawString("DURACION FASE DE ESTER.:", font, XBrushes.Black, new XRect(20, 605, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotal + " " + "min.s", font, XBrushes.Black, new XRect(160, 605, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("F(T,z) MIN.:", font, XBrushes.Black, new XRect(20, 620, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.FtzMin, font, XBrushes.Black, new XRect(160, 620, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("F(T,z) MAX.:", font, XBrushes.Black, new XRect(20, 635, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.FtzMax, font, XBrushes.Black, new XRect(160, 635, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("Dif F(T,z):", font, XBrushes.Black, new XRect(20, 650, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DifMaxMin, font, XBrushes.Black, new XRect(160, 650, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.AperturaPuerta, font, XBrushes.Black, new XRect(20, 665, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("FIRMA OPERADOR        _______________________ ", font, XBrushes.Black, new XRect(20, 705, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("FIRMA GAR.DE CALID.   _______________________ ", font, XBrushes.Black, new XRect(20, 745, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                if (q.ErrorCiclo == "")
                {
                    graph.DrawString("Pág 1 de 1  ", font, XBrushes.Black, new XRect(480, 800, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                   
                }
                else
                {
                    string[] error = q.ErrorCiclo.Split('\n');
                    if (error.Count() > 24)
                    {
                        PdfPage pdfPage2 = pdf.AddPage();
                        XGraphics graph2 = XGraphics.FromPdfPage(pdfPage2);
                        graph2.DrawString("ID. MAQUINA:" + "  " + q.IdAutoclave, font, XBrushes.Black, new XRect(20, 5, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph2.DrawString("N.PROGRESIVO:" + "  " + q.NumeroCiclo, font, XBrushes.Black, new XRect(140, 5, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph2.DrawString("Informe de ciclo de esterilización", font, XBrushes.Black, new XRect(270, 5, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph2.DrawString("Impreso: " + DateTime.Now, font, XBrushes.Black, new XRect(440, 5, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph2.DrawString("Por: " + _httpContextAccessor.HttpContext.Session.GetString("SessionName"), font, XBrushes.Black, new XRect(440, 15, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph.DrawString("ALARMAS:", fontDos, XBrushes.Black, new XRect(340, 530, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        for (int i = 0; i < error.Length; i++)
                        {
                            if (i >= 0 && i <= 24)
                            {
                                graph.DrawString(error[i], fontDos, XBrushes.Black, new XRect(340, 540 + i * 10, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            }
                            if (i > 24)
                            {
                                graph2.DrawString(error[i], fontDos, XBrushes.Black, new XRect(50, 30 + (i - 25) * 10, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                            }

                        }
                        graph.DrawString("Pág 1 de 2  ", font, XBrushes.Black, new XRect(480, 800, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        graph2.DrawString("Pág 2 de 2  ", font, XBrushes.Black, new XRect(480, 800, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                    }
                    else
                    {
                        graph.DrawString("ALARMAS:", fontDos, XBrushes.Black, new XRect(340, 530, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        tf.DrawString(q.ErrorCiclo, fontDos, XBrushes.Black, rect, XStringFormats.TopLeft);
                        graph.DrawString("Pág 1 de 1  ", font, XBrushes.Black, new XRect(480, 800, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    }



                }
              
                string rut = _config["OptionalSettings:Pdf"] + "\\PDF";
                Directory.CreateDirectory(rut);
                string ruta = rut + "\\archivo1.pdf";
                pdf.Save(ruta);

            }
        }
    }
}
