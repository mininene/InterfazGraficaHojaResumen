﻿using Microsoft.AspNetCore.Http;
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
    public class PrinterNueveDiezAS : IPrinterNueveDiezAS
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public PrinterNueveDiezAS(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public void printNueveDiezAS(int? id)
        {
            var q = _context.CiclosSabiDos.FirstOrDefault(m => m.Id == id);

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
               


                rect = new XRect(340, 520, 250, 220);
                recta = new XRect(130, 105, 430, 20); //25/06/2021
                graph.DrawRectangle(XBrushes.White, rect);
                graph.DrawRectangle(XBrushes.White, recta);
                tf.Alignment = XParagraphAlignment.Justify;


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

                graph.DrawString("FASE 2:  " + q.Fase2, font, XBrushes.Black, new XRect(20, 175, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 175, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF2, negrita, XBrushes.Black, new XRect(320, 175, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("min.s", font, XBrushes.Black, new XRect(350, 175, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("<--[  ]", negrita, XBrushes.Black, new XRect(375, 175, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                DateTime temp = DateTime.ParseExact(q.Tinicio, "dd/MM/yy HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime fechaInicioFormat = DateTime.ParseExact(temp.ToString("dd/MM/yyyy HH:mm:ss"), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                graph.DrawString("I " + " " + fechaInicioFormat, negrita, XBrushes.Black, new XRect(460, 175, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 190, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.Tif2, font, XBrushes.Black, new XRect(230, 190, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.TisubF2, font, XBrushes.Black, new XRect(460, 190, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 205, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                var tff2 = q.Tff2.Split(new String[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                graph.DrawString(tff2[0], font, XBrushes.Black, new XRect(230, 205, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(tff2[1].Trim(), negrita, XBrushes.Black, new XRect(263, 205, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString($"{tff2[2]}  {tff2[3]}  {tff2[4]}  {tff2[5]}  {tff2[6]}", font, XBrushes.Black, new XRect(290, 205, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.TfsubF2, font, XBrushes.Black, new XRect(460, 205, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                //graph.DrawString(q.Tff2.Substring(0, 6), font, XBrushes.Black, new XRect(230, 205, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                //graph.DrawString(q.Tff2.Substring(6, 6), negrita, XBrushes.Black, new XRect(255, 205, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                //graph.DrawString(q.Tff2.Substring(11), font, XBrushes.Black, new XRect(275, 205, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                graph.DrawString("FASE 3:  " + q.Fase3, font, XBrushes.Black, new XRect(20, 225, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 225, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF3, negrita, XBrushes.Black, new XRect(320, 225, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("min.s", font, XBrushes.Black, new XRect(350, 225, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("<--[  ]", negrita, XBrushes.Black, new XRect(375, 225, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10 ", font, XBrushes.Black, new XRect(20, 240, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                var tif3 = q.Tif3.Split(new String[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                graph.DrawString(tif3[0], font, XBrushes.Black, new XRect(230, 240, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(tif3[1].Trim(), negrita, XBrushes.Black, new XRect(263, 240, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString($"{tif3[2]}  {tif3[3]}  {tif3[4]}  {tif3[5]}  {tif3[6]}", font, XBrushes.Black, new XRect(290, 240, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.TisubF3, font, XBrushes.Black, new XRect(460, 240, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 255, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                var tff3 = q.Tff3.Split(new String[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                graph.DrawString(tff3[0], font, XBrushes.Black, new XRect(230, 255, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(tff3[1].Trim(), negrita, XBrushes.Black, new XRect(263, 255, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString($"{tff3[2]}  {tff3[3]}  {tff3[4]}  {tff3[5]}  {tff3[6]}", font, XBrushes.Black, new XRect(290, 255, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.TfsubF3.Substring(0, 2), font, XBrushes.Black, new XRect(460, 255, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.TfsubF3.Substring(2) + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(465, 255, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("FASE 4:  " + q.Fase4, font, XBrushes.Black, new XRect(20, 275, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 275, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF4 + " " + "min.s", font, XBrushes.Black, new XRect(320, 275, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 290, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.Tif4, font, XBrushes.Black, new XRect(230, 290, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.TisubF4, font, XBrushes.Black, new XRect(460, 290, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("FASE 5:  " + q.Fase5, font, XBrushes.Black, new XRect(20, 305, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 305, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF5 + " " + "min.s", font, XBrushes.Black, new XRect(320, 305, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("FASE 6:  " + q.Fase6, font, XBrushes.Black, new XRect(20, 320, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 320, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF6 + " " + "min.s", font, XBrushes.Black, new XRect(320, 320, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("FASE 7:  " + q.Fase7A, font, XBrushes.Black, new XRect(20, 335, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 335, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF7a + " " + "min.s", font, XBrushes.Black, new XRect(320, 335, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("FASE 8:  " + q.Fase8A, font, XBrushes.Black, new XRect(20, 350, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 350, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF8a + " " + "min.s", font, XBrushes.Black, new XRect(320, 350, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("FASE 7:  " + q.Fase7B, font, XBrushes.Black, new XRect(20, 365, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 365, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF7b + " " + "min.s", font, XBrushes.Black, new XRect(320, 365, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("FASE 8:  " + q.Fase8B, font, XBrushes.Black, new XRect(20, 380, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 380, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF8a + " " + "min.s", font, XBrushes.Black, new XRect(320, 380, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("FASE 9:  " + q.Fase9, font, XBrushes.Black, new XRect(20, 395, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 395, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF9 + " " + "min.s", font, XBrushes.Black, new XRect(320, 395, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10 ", font, XBrushes.Black, new XRect(20, 410, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.Tif9, font, XBrushes.Black, new XRect(230, 410, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.TisubF9, font, XBrushes.Black, new XRect(460, 410, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DATOS FINALES DE FASE:", font, XBrushes.Black, new XRect(20, 425, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.Tff9, font, XBrushes.Black, new XRect(230, 425, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("FASE 10: " + q.Fase10, font, XBrushes.Black, new XRect(20, 440, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 440, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF10 + " " + "min.s", font, XBrushes.Black, new XRect(320, 440, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("FASE 11: " + q.Fase11, font, XBrushes.Black, new XRect(20, 455, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("DURAC.TOTAL FASE:", font, XBrushes.Black, new XRect(230, 455, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotalF11 + " " + "min.s", font, XBrushes.Black, new XRect(320, 455, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("FASE 12: FIN DE CICLO         TIEMPO TP  TE2  TE3  TE4  TE9 TE10", font, XBrushes.Black, new XRect(20, 475, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                var tif12 = q.Tif12.Split(new String[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                graph.DrawString(tif12[0] + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(20, 490, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString($"{tif12[1]}  {tif12[2]}  {tif12[3]}  {tif12[4]}  {tif12[5]}  {tif12[6]}", font, XBrushes.Black, new XRect(160, 490, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.TisubF12.Substring(0, 2), font, XBrushes.Black, new XRect(460, 490, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.TisubF12.Substring(2) + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(465, 490, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);



                graph.DrawString("HORA COMIEN.PROGR :", font, XBrushes.Black, new XRect(20, 510, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.HoraInicio, negrita, XBrushes.Black, new XRect(160, 510, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("HORA FIN.PROGR :", font, XBrushes.Black, new XRect(20, 525, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.HoraFin, negrita, XBrushes.Black, new XRect(160, 525, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                //graph.DrawString("ESTERILIZACION N.:", font, XBrushes.Black, new XRect(20, 540, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                //graph.DrawString(q.EsterilizacionN, font, XBrushes.Black, new XRect(160, 540, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                if (q.EsterilizacionN == "")
                {
                    graph.DrawString("ESTERILIZACION:", font, XBrushes.Black, new XRect(20, 540, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString("FALLIDA", font, XBrushes.Black, new XRect(160, 540, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                }
                else
                {
                    graph.DrawString("ESTERILIZACION N.:", font, XBrushes.Black, new XRect(20, 540, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                    graph.DrawString(q.EsterilizacionN, font, XBrushes.Black, new XRect(160, 540, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


                }

                graph.DrawString("TEMP.MIN.ESTERILIZACION:", font, XBrushes.Black, new XRect(20, 555, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("°C  ", font, XBrushes.Black, new XRect(160, 555, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.Tminima + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(175, 555, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("TEMP.MAX.ESTERILIZACION:", font, XBrushes.Black, new XRect(20, 570, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("°C  ", font, XBrushes.Black, new XRect(160, 570, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.Tmaxima + " " + "<--[  ]", negrita, XBrushes.Black, new XRect(175, 570, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);



                graph.DrawString("DURACION FASE DE ESTER.:", font, XBrushes.Black, new XRect(20, 585, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DuracionTotal + " " + "min.s", font, XBrushes.Black, new XRect(160, 585, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("F(T,z) MIN.:", font, XBrushes.Black, new XRect(20, 600, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.FtzMin, font, XBrushes.Black, new XRect(160, 600, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("F(T,z) MAX.:", font, XBrushes.Black, new XRect(20, 615, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.FtzMax, font, XBrushes.Black, new XRect(160, 615, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("Dif F(T,z):", font, XBrushes.Black, new XRect(20, 630, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.DifMaxMin, font, XBrushes.Black, new XRect(160, 630, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.AperturaPuerta, font, XBrushes.Black, new XRect(20, 645, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("FIRMA OPERADOR        _______________________ ", font, XBrushes.Black, new XRect(20, 685, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("FIRMA GAR.DE CALID.   _______________________ ", font, XBrushes.Black, new XRect(20, 725, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);


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
                        graph.DrawString("ALARMAS:", fontDos, XBrushes.Black, new XRect(340, 510, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                        for (int i = 0; i < error.Length; i++)
                        {
                            if (i >= 0 && i <= 24)
                            {
                                graph.DrawString(error[i], fontDos, XBrushes.Black, new XRect(340, 520 + i * 10, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
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
                        graph.DrawString("ALARMAS:", fontDos, XBrushes.Black, new XRect(340, 510, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        tf.DrawString(q.ErrorCiclo, fontDos, XBrushes.Black, rect, XStringFormats.TopLeft);
                        graph.DrawString("Pág 1 de 1  ", font, XBrushes.Black, new XRect(480, 800, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    }

                }

                //string rut = @"\\essaappserver01\HojaResumen\old\archivo1.pdf";
                //string rut = @"C:\Program Files\HojaResumen\old\archivo1.pdf";
                //pdf.Save(rut);

                string rut = _config["OptionalSettings:Pdf"] + "\\PDF";
                Directory.CreateDirectory(rut);
                string ruta = rut + "\\archivo2.pdf";
                pdf.Save(ruta);




            }
        }
    }
}
