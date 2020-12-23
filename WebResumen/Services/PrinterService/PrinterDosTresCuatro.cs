﻿using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using WebResumen.Models;
using Microsoft.AspNetCore.Http;

namespace WebResumen.Services.PrinterService
{
    public class PrinterDosTresCuatro : IPrinterDosTresCuatro
    {
        private readonly AppDbContext _context;
        public PrinterDosTresCuatro(AppDbContext context)
        {
            _context = context;
        }
        public void printDosTresCuatro(int? id)
        {
            var q = _context.CiclosAutoclaves.FirstOrDefault(m => m.Id == id);

            PrintDocument _pr = new PrintDocument();
            PrintController _controller = new StandardPrintController();
            PrinterSettings _newSettings = new System.Drawing.Printing.PrinterSettings();

            System.Drawing.Font _font = new System.Drawing.Font("Verdana", 8, FontStyle.Regular);
            System.Drawing.Font _fontDos = new System.Drawing.Font("verdana", 7, FontStyle.Regular);
            System.Drawing.Font _negrita = new System.Drawing.Font("Verdana", 8, FontStyle.Bold);
            SolidBrush _solid = new SolidBrush(Color.Black);

            StringFormat _tf = new StringFormat();
            StringFormat _td = new StringFormat();
            RectangleF _rect = new RectangleF();

            _rect = new RectangleF(450, 760, 350, 300);
            _tf.Alignment = StringAlignment.Near;
            _td.FormatFlags = StringFormatFlags.LineLimit;
            _td.Trimming = StringTrimming.Word;
            _pr.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);



            _pr.PrinterSettings.PrinterName = @"\\essafileprint01\#ADMICOPY (ESSAFILEPRINT01)";
            _pr.PrintController = _controller;
            _pr.Print(); //start the print
            _pr.Dispose();
            void printDoc_PrintPage(object sender, PrintPageEventArgs e)
            {
                IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
                Graphics graph = e.Graphics;


                graph.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                graph.DrawString("ID. MAQUINA:" + "  " + q.IdAutoclave, _font, _solid, new RectangleF(20, 5, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("N.PROGRESIVO:" + "  " + q.NumeroCiclo, _font, _solid, new RectangleF(190, 5, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("Informe de ciclo de esterilización", _font, _solid, new RectangleF(360, 5, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("Impreso: " + DateTime.Now, _font, _solid, new RectangleF(580, 5, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("Impreso por: "+ _httpContextAccessor.HttpContext.Session.GetString("SessionName"), _font, _solid, new RectangleF(580, 20, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                graph.DrawString("PROGRAMA:", _font, _solid, new RectangleF(20, 30, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Programa + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 30, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.IdSeccion, _font, _solid, new RectangleF(20, 45, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                graph.DrawString("PROGRAMADOR:", _font, _solid, new RectangleF(20, 70, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Programador, _font, _solid, new RectangleF(180, 70, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("OPERADOR:", _font, _solid, new RectangleF(20, 85, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Operador, _font, _solid, new RectangleF(180, 85, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("CODIGO PRODUCTO:", _font, _solid, new RectangleF(20, 100, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.CodigoProducto + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 100, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("N.LOTE:", _font, _solid, new RectangleF(20, 115, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Lote + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 115, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("ID. MAQUINA:", _font, _solid, new RectangleF(20, 130, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.IdAutoclave + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 130, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("NOTAS:", _font, _solid, new RectangleF(20, 145, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Notas.Trim() + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 145, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                graph.DrawString("MODELO:", _font, _solid, new RectangleF(20, 170, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Modelo, _font, _solid, new RectangleF(180, 170, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("N.PROGRESIVO:", _font, _solid, new RectangleF(20, 185, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.NumeroCiclo + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 185, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                graph.DrawString("FASE 1:  " + q.Fase1, _font, _solid, new RectangleF(20, 210, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 210, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.DuracionTotalF1 + " " + "min.s", _font, _solid, new RectangleF(420, 210, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                graph.DrawString("FASE 2:  " + q.Fase2, _font, _solid, new RectangleF(20, 230, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 230, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.DuracionTotalF2 + " " + "min.s", _font, _solid, new RectangleF(420, 230, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                graph.DrawString("FASE 3:  " + q.Fase3, _font, _solid, new RectangleF(20, 250, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 250, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.DuracionTotalF3 + " " + "min.s", _font, _solid, new RectangleF(420, 250, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                graph.DrawString("FASE 4:  " + q.Fase4, _font, _solid, new RectangleF(20, 270, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 270, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.DuracionTotalF4 + " " + "min.s", _font, _solid, new RectangleF(420, 270, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                graph.DrawString("FASE 5:  " + q.Fase5, _font, _solid, new RectangleF(20, 295, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 295, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.DuracionTotalF5, _negrita, _solid, new RectangleF(420, 295, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("min.s", _font, _solid, new RectangleF(460, 295, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("<--[  ]", _negrita, _solid, new RectangleF(495, 295, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                graph.DrawString(q.Tinicio, _negrita, _solid, new RectangleF(600, 295, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", _font, _solid, new RectangleF(20, 315, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Tif5, _font, _solid, new RectangleF(300, 315, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.TisubF5, _font, _solid, new RectangleF(600, 315, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10 ", _font, _solid, new RectangleF(20, 335, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                //graph.DrawString(q.TFF5, font, XBrushes.Black, new XRect(230, 250, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.Tff5.Substring(0, 6), _font, _solid, new RectangleF(300, 335, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Tff5.Substring(6, 6).Trim(), _negrita, _solid, new RectangleF(345, 335, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Tff5.Substring(12), _font, _solid, new RectangleF(370, 335, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.TfsubF5, _font, _solid, new RectangleF(600, 335, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                graph.DrawString("FASE 6:  " + q.Fase6, _font, _solid, new RectangleF(20, 360, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 360, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.DuracionTotalF6, _negrita, _solid, new RectangleF(420, 360, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("min.s", _font, _solid, new RectangleF(460, 360, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("<--[  ]", _negrita, _solid, new RectangleF(495, 360, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", _font, _solid, new RectangleF(20, 380, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                //graph.DrawString(q.TIF6, font, XBrushes.Black, new XRect(230, 285, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString(q.Tif6.Substring(0, 6), _font, _solid, new RectangleF(300, 380, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Tif6.Substring(6, 6).Trim(), _negrita, _solid, new RectangleF(345, 380, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Tif6.Substring(12), _font, _solid, new RectangleF(370, 380, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.TisubF6, _font, _solid, new RectangleF(600, 380, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", _font, _solid, new RectangleF(20, 400, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Tff6.Substring(0, 6), _font, _solid, new RectangleF(300, 400, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Tff6.Substring(6, 6).Trim(), _negrita, _solid, new RectangleF(345, 400, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Tff6.Substring(12), _font, _solid, new RectangleF(370, 400, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.TfsubF6.Substring(0, 2), _font, _solid, new RectangleF(600, 400, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.TfsubF6.Substring(2) + " " + "<--[  ]", _negrita, _solid, new RectangleF(605, 400, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                graph.DrawString("FASE 7:  " + q.Fase7, _font, _solid, new RectangleF(20, 425, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 425, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                graph.DrawString(q.DuracionTotalF7, _font, _solid, new RectangleF(420, 425, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("min.s", _font, _solid, new RectangleF(460, 425, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("<--[  ]", _negrita, _solid, new RectangleF(495, 425, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", _font, _solid, new RectangleF(20, 445, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Tif7.Substring(0, 6), _font, _solid, new RectangleF(300, 445, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Tif7.Substring(6, 6), _negrita, _solid, new RectangleF(345, 445, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Tif7.Substring(12), _font, _solid, new RectangleF(395, 445, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.TisubF7, _font, _solid, new RectangleF(600, 445, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);


                graph.DrawString("FASE 8:  " + q.Fase8, _font, _solid, new RectangleF(20, 470, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 470, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.DuracionTotalF8 + " " + "min.s", _font, _solid, new RectangleF(420, 470, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", _font, _solid, new RectangleF(20, 490, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Tif8, _font, _solid, new RectangleF(300, 490, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.TisubF8, _font, _solid, new RectangleF(600, 490, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("DATOS FINALES DE FASE:", _font, _solid, new RectangleF(20, 510, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Tff8, _font, _solid, new RectangleF(300, 510, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                graph.DrawString("FASE 9:  " + q.Fase9, _font, _solid, new RectangleF(20, 535, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 535, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.DuracionTotalF9 + " " + "min.s", _font, _solid, new RectangleF(420, 535, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", _font, _solid, new RectangleF(20, 555, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Tif9, _font, _solid, new RectangleF(300, 555, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.TisubF9, _font, _solid, new RectangleF(600, 555, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                graph.DrawString("DATOS FINALES DE FASE:", _font, _solid, new RectangleF(20, 575, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Tff9, _font, _solid, new RectangleF(300, 575, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                graph.DrawString("FASE 10: " + q.Fase10, _font, _solid, new RectangleF(20, 600, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 600, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.DuracionTotalF10 + " " + "min.s", _font, _solid, new RectangleF(420, 600, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);


                graph.DrawString("FASE 11: " + q.Fase11, _font, _solid, new RectangleF(20, 625, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 625, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.DuracionTotalF11 + " " + "min.s", _font, _solid, new RectangleF(420, 625, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                graph.DrawString("FASE 12: " + q.Fase12, _font, _solid, new RectangleF(20, 650, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 650, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.DuracionTotalF12 + " " + "min.s", _font, _solid, new RectangleF(420, 650, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                graph.DrawString("FASE 13: FIN DE CICLO         TIEMPO TPO-C AG. PRES. TE2 TE3 TE4 TE9 TE10", _font, _solid, new RectangleF(20, 675, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Tff13.Substring(0, 6), _font, _solid, new RectangleF(20, 695, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.TiempoCiclo, _font, _solid, new RectangleF(80, 695, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Tff13.Substring(6), _font, _solid, new RectangleF(200, 695, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.TfsubF13.Substring(0, 2), _font, _solid, new RectangleF(600, 695, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.TfsubF13.Substring(2), _font, _solid, new RectangleF(605, 695, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                graph.DrawString("HORA COMIEN.PROGR :", _font, _solid, new RectangleF(20, 740, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.HoraInicio, _negrita, _solid, new RectangleF(200, 740, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("HORA FIN.PROGR :", _font, _solid, new RectangleF(20, 760, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.HoraFin, _negrita, _solid, new RectangleF(200, 760, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);


                if (q.EsterilizacionN == "")
                {
                    graph.DrawString("ESTERILIZACION:", _font, _solid, new RectangleF(20, 780, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                    graph.DrawString("FALLIDA", _font, _solid, new RectangleF(200, 780, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                }
                else
                {
                    graph.DrawString("ESTERILIZACION N.:", _font, _solid, new RectangleF(20, 780, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                    graph.DrawString(q.EsterilizacionN, _font, _solid, new RectangleF(200, 780, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                }

                graph.DrawString("TEMP.MIN.ESTERILIZACION:", _font, _solid, new RectangleF(20, 800, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("°C  ", _font, _solid, new RectangleF(200, 800, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Tminima + " " + "<--[  ]", _negrita, _solid, new RectangleF(220, 800, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("TEMP.MAX.ESTERILIZACION:", _font, _solid, new RectangleF(20, 820, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("°C  ", _font, _solid, new RectangleF(200, 820, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.Tmaxima + " " + "<--[  ]", _negrita, _solid, new RectangleF(220, 820, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                graph.DrawString("DURACION FASE DE ESTER.:", _font, _solid, new RectangleF(20, 840, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.DuracionTotal + " " + "min.s", _font, _solid, new RectangleF(200, 840, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("F(T,z) MIN.:", _font, _solid, new RectangleF(20, 860, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.FtzMin, _font, _solid, new RectangleF(200, 860, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("F(T,z) MAX.:", _font, _solid, new RectangleF(20, 880, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.FtzMax, _font, _solid, new RectangleF(200, 880, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("Dif F(T,z):", _font, _solid, new RectangleF(20, 900, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.DifMaxMin, _font, _solid, new RectangleF(200, 900, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString(q.AperturaPuerta, _font, _solid, new RectangleF(20, 920, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("FIRMA OPERADOR        _______________________ ", _font, _solid, new RectangleF(20, 960, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                graph.DrawString("FIRMA GAR.DE CALID.   _______________________ ", _font, _solid, new RectangleF(20, 1020, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                if (q.ErrorCiclo == "")
                {
                    graph.DrawString("ALARMAS:", _fontDos, _solid, new RectangleF(450, 740, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);
                    graph.DrawString("* NO EXISTEN ALARMAS REGISTRADAS", _fontDos, _solid, new RectangleF(450, 760, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                }
                else
                {
                    graph.DrawString("ALARMAS:", _fontDos, _solid, new RectangleF(450, 740, _pr.DefaultPageSettings.PrintableArea.Width, _pr.DefaultPageSettings.PrintableArea.Height), _tf);

                    graph.DrawString(q.ErrorCiclo, _fontDos, _solid, _rect, _td);

                }


            }



        }
    }
}
