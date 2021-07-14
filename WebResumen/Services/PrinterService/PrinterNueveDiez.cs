using Microsoft.AspNetCore.Http;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using WebResumen.Models;

namespace WebResumen.Services.PrinterService
{
    public class PrinterNueveDiez : IPrinterNueveDiez
    {
        private readonly AppDbContext _context;
        public PrinterNueveDiez(AppDbContext context)
        {
            _context = context;
        }
        public void printNueveDiez(int? id)
        {
            var q = _context.CiclosSabiDos.FirstOrDefault(m => m.Id == id);
            var impresora = _context.Parametros.Select(t => t.ImpresoraSabiDos).FirstOrDefault();



            PrintDocument _pr = new PrintDocument();
            PrintController _controller = new StandardPrintController();
            PrinterSettings _newSettings = new System.Drawing.Printing.PrinterSettings();

            System.Drawing.Font _font = new System.Drawing.Font("Verdana", 8, FontStyle.Regular);
            System.Drawing.Font _fontDos = new System.Drawing.Font("verdana", 7, FontStyle.Regular);
            System.Drawing.Font _negrita = new System.Drawing.Font("Verdana", 8, FontStyle.Bold);
            System.Drawing.Font _negritaDos = new System.Drawing.Font("Verdana", 7, FontStyle.Bold);
            SolidBrush _solid = new SolidBrush(Color.Black);

            StringFormat _tf = new StringFormat();
            StringFormat _td = new StringFormat();
            RectangleF _rect = new RectangleF();
            RectangleF _recta = new RectangleF();

            _rect = new RectangleF(450, 760, 350, 300);
            _recta = new RectangleF(180, 145, 600, 30);//25/06/2021
            _tf.Alignment = StringAlignment.Near;
            _td.FormatFlags = StringFormatFlags.LineLimit;
            _td.Trimming = StringTrimming.Word;
            int page = 1;
            var width = _pr.DefaultPageSettings.PrintableArea.Width;
            var height = _pr.DefaultPageSettings.PrintableArea.Height;
            _pr.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);



            //_pr.PrinterSettings.PrinterName = @"\\essafileprint01\#ADMICOLOR (ESSAFILEPRINT01)";
            // _pr.PrinterSettings.PrinterName = "PDFCreator";
            _pr.PrinterSettings.PrinterName = impresora.ToString();
            _pr.PrintController = _controller;
            _pr.Print(); //start the print
            _pr.Dispose();

            void printDoc_PrintPage(object sender, PrintPageEventArgs e)
            {
                IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
                Graphics graph = e.Graphics;


                graph.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                graph.DrawString("ID. MAQUINA:" + "  " + q.IdAutoclave, _font, _solid, new RectangleF(20, 5, width, height), _tf);
                graph.DrawString("N.PROGRESIVO:" + "  " + q.NumeroCiclo, _font, _solid, new RectangleF(190, 5, width, height), _tf);
                graph.DrawString("Informe de ciclo de esterilización", _font, _solid, new RectangleF(360, 5, width, height), _tf);
                graph.DrawString("Impreso: " + DateTime.Now, _font, _solid, new RectangleF(580, 5, width, height), _tf);
                graph.DrawString("Por: " + _httpContextAccessor.HttpContext.Session.GetString("SessionName"), _font, _solid, new RectangleF(580, 20, width, height), _tf);

                graph.DrawString("PROGRAMA:", _font, _solid, new RectangleF(20, 30, width, height), _tf);
                graph.DrawString(q.Programa + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 30, width, height), _tf);
                graph.DrawString(q.IdSeccion, _font, _solid, new RectangleF(20, 45, width, height), _tf);

                graph.DrawString("PROGRAMADOR:", _font, _solid, new RectangleF(20, 70, width, height), _tf);
                graph.DrawString(q.Programador, _font, _solid, new RectangleF(180, 70, width, height), _tf);
                graph.DrawString("OPERADOR:", _font, _solid, new RectangleF(20, 85, width, height), _tf);
                graph.DrawString(q.Operador, _font, _solid, new RectangleF(180, 85, width, height), _tf);
                graph.DrawString("CODIGO PRODUCTO:", _font, _solid, new RectangleF(20, 100, width, height), _tf);
                graph.DrawString(q.CodigoProducto + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 100, width, height), _tf);
                graph.DrawString("N.LOTE:", _font, _solid, new RectangleF(20, 115, width, height), _tf);
                graph.DrawString(q.Lote + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 115, width, height), _tf);
                graph.DrawString("ID. MAQUINA:", _font, _solid, new RectangleF(20, 130, width, height), _tf);
                graph.DrawString(q.IdAutoclave + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 130, width, height), _tf);
                graph.DrawString("NOTAS:", _font, _solid, new RectangleF(20, 145, width, height), _tf);
                //graph.DrawString(q.Notas.Trim() + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 145, width, height), _tf);
                graph.DrawString(q.Notas.Trim() + " " + "<--[  ]", _negritaDos, _solid, _recta, _td);

                graph.DrawString("MODELO:", _font, _solid, new RectangleF(20, 170, width, height), _tf);
                graph.DrawString(q.Modelo, _font, _solid, new RectangleF(180, 170, width, height), _tf);
                graph.DrawString("N.PROGRESIVO:", _font, _solid, new RectangleF(20, 185, width, height), _tf);
                graph.DrawString(q.NumeroCiclo + " " + "<--[  ]", _negrita, _solid, new RectangleF(180, 185, width, height), _tf);

                graph.DrawString("FASE 1:  " + q.Fase1, _font, _solid, new RectangleF(20, 210, width, height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 210, width, height), _tf);
                graph.DrawString(q.DuracionTotalF1 + " " + "min.s", _font, _solid, new RectangleF(420, 210, width, height), _tf);

                graph.DrawString("FASE 2:  " + q.Fase2, _font, _solid, new RectangleF(20, 230, width, height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 230, width, height), _tf);
                graph.DrawString(q.DuracionTotalF2, _negrita, _solid, new RectangleF(420, 230, width, height), _tf);
                graph.DrawString("min.s", _font, _solid, new RectangleF(460, 230, width, height), _tf);
                graph.DrawString("< --[  ]", _negrita, _solid, new RectangleF(495, 230, width, height), _tf);
               // graph.DrawString("I " + " " + q.Tinicio, _negrita, _solid, new RectangleF(600, 230, width, height), _tf);

                DateTime temp = DateTime.ParseExact(q.Tinicio, "dd/MM/yy HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime fechaInicioFormat = DateTime.ParseExact(temp.ToString("dd/MM/yyyy HH:mm:ss"), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                graph.DrawString("I " + " " + fechaInicioFormat, _negrita, _solid, new RectangleF(600, 230, width, height), _tf);

                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", _font, _solid, new RectangleF(20, 250, width, height), _tf);
                graph.DrawString(q.Tif2, _font, _solid, new RectangleF(300, 250, width, height), _tf);
                graph.DrawString(q.TisubF2, _font, _solid, new RectangleF(600, 250, width, height), _tf);


                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10 ", _font, _solid, new RectangleF(20, 270, width, height), _tf);
                var tff2 = q.Tff2.Split(new String[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                graph.DrawString(tff2[0], _font, _solid, new RectangleF(300, 270, width, height), _tf);
                graph.DrawString(tff2[1].Trim(), _negrita, _solid, new RectangleF(345, 270, width, height), _tf);
                graph.DrawString($"{tff2[2]}  {tff2[3]}  {tff2[4]}  {tff2[5]}  {tff2[6]}", _font, _solid, new RectangleF(380, 270, width, height), _tf);
                //graph.DrawString(q.Tff2.Substring(0, 6), _font, _solid, new RectangleF(300, 270, width, height), _tf);
                //graph.DrawString(q.Tff2.Substring(6, 6).Trim(), _negrita, _solid, new RectangleF(345, 270, width, height), _tf);
                //graph.DrawString(q.Tff2.Substring(12), _font, _solid, new RectangleF(370, 270, width, height), _tf);
                graph.DrawString(q.TfsubF2, _font, _solid, new RectangleF(600, 270, width, height), _tf);



                graph.DrawString("FASE 3:  " + q.Fase3, _font, _solid, new RectangleF(20, 295, width, height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 295, width, height), _tf);
                graph.DrawString(q.DuracionTotalF3, _negrita, _solid, new RectangleF(420, 295, width, height), _tf);
                graph.DrawString("min.s", _font, _solid, new RectangleF(460, 295, width, height), _tf);
                graph.DrawString("< --[  ]", _negrita, _solid, new RectangleF(495, 295, width, height), _tf);

                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10 ", _font, _solid, new RectangleF(20, 315, width, height), _tf);
                var tif3 = q.Tif3.Split(new String[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                graph.DrawString(tif3[0], _font, _solid, new RectangleF(300, 315, width, height), _tf);
                graph.DrawString(tif3[1].Trim(), _negrita, _solid, new RectangleF(345, 315, width, height), _tf);
                graph.DrawString($"{tif3[2]}  {tif3[3]}  {tif3[4]}  {tif3[5]}  {tif3[6]}", _font, _solid, new RectangleF(380, 315, width, height), _tf);
                 graph.DrawString(q.TisubF3, _font, _solid, new RectangleF(600, 315, width, height), _tf); //
                //graph.DrawString(q.Tif3.Substring(0, 6), _font, _solid, new RectangleF(300, 315, width, height), _tf);
                //graph.DrawString(q.Tif3.Substring(6, 6).Trim(), _negrita, _solid, new RectangleF(345, 315, width, height), _tf);
                //graph.DrawString(q.Tif3.Substring(12), _font, _solid, new RectangleF(370, 315, width, height), _tf);
               


                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10 ", _font, _solid, new RectangleF(20, 335, width, height), _tf);
                var tff3 = q.Tff3.Split(new String[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                graph.DrawString(tff3[0], _font, _solid, new RectangleF(300, 335, width, height), _tf);
                graph.DrawString(tff3[1].Trim(), _negrita, _solid, new RectangleF(345, 335, width, height), _tf);
                graph.DrawString($"{tff3[2]}  {tff3[3]}  {tff3[4]}  {tff3[5]}  {tff3[6]}", _font, _solid, new RectangleF(380, 335, width, height), _tf);
                graph.DrawString(q.TfsubF3.Substring(0, 2), _font, _solid, new RectangleF(600, 335, width, height), _tf);
                graph.DrawString(q.TfsubF3.Substring(2) + " " + "<--[  ]", _negrita, _solid, new RectangleF(605, 335, width, height), _tf);

                //graph.DrawString(q.Tff3.Substring(0, 6), _font, _solid, new RectangleF(300, 335, width, height), _tf);
                //graph.DrawString(q.Tff3.Substring(6, 6).Trim(), _negrita, _solid, new RectangleF(345, 335, width, height), _tf);
                //graph.DrawString(q.Tff3.Substring(12), _font, _solid, new RectangleF(370, 335, width, height), _tf);




                graph.DrawString("FASE 4:  " + q.Fase4, _font, _solid, new RectangleF(20, 360, width, height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 360, width, height), _tf);
                graph.DrawString(q.DuracionTotalF4 + " " + "min.s", _font, _solid, new RectangleF(420, 360, width, height), _tf);
                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", _font, _solid, new RectangleF(20, 380, width, height), _tf);
                graph.DrawString(q.Tif4, _font, _solid, new RectangleF(300, 380, width, height), _tf);
                graph.DrawString(q.TisubF4, _font, _solid, new RectangleF(600, 380, width, height), _tf);

                graph.DrawString("FASE 5: " + q.Fase5, _font, _solid, new RectangleF(20, 405, width, height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 405, width, height), _tf);
                graph.DrawString(q.DuracionTotalF5 + " " + "min.s", _font, _solid, new RectangleF(420, 405, width, height), _tf);

                graph.DrawString("FASE 6: " + q.Fase6, _font, _solid, new RectangleF(20, 430, width, height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 430, width, height), _tf);
                graph.DrawString(q.DuracionTotalF6 + " " + "min.s", _font, _solid, new RectangleF(420, 430, width, height), _tf);

                graph.DrawString("FASE 7: " + q.Fase7A, _font, _solid, new RectangleF(20, 455, width, height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 455, width, height), _tf);
                graph.DrawString(q.DuracionTotalF7a + " " + "min.s", _font, _solid, new RectangleF(420, 455, width, height), _tf);

                graph.DrawString("FASE 8: " + q.Fase8A, _font, _solid, new RectangleF(20, 480, width, height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 480, width, height), _tf);
                graph.DrawString(q.DuracionTotalF8a + " " + "min.s", _font, _solid, new RectangleF(420, 480, width, height), _tf);

                graph.DrawString("FASE 7: " + q.Fase7B, _font, _solid, new RectangleF(20, 505, width, height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 505, width, height), _tf);
                graph.DrawString(q.DuracionTotalF7b + " " + "min.s", _font, _solid, new RectangleF(420, 505, width, height), _tf);

                graph.DrawString("FASE 8: " + q.Fase8B, _font, _solid, new RectangleF(20, 530, width, height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 530, width, height), _tf);
                graph.DrawString(q.DuracionTotalF8b + " " + "min.s", _font, _solid, new RectangleF(420, 530, width, height), _tf);


                graph.DrawString("FASE 9:  " + q.Fase9, _font, _solid, new RectangleF(20, 555, width, height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 555, width, height), _tf);
                graph.DrawString(q.DuracionTotalF9 + " " + "min.s", _font, _solid, new RectangleF(420, 555, width, height), _tf);
                graph.DrawString("TIEMPO TP  TE2  TE3  TE4  TE9 TE10", _font, _solid, new RectangleF(20, 575, width, height), _tf);
                graph.DrawString(q.Tif9, _font, _solid, new RectangleF(300, 575, width, height), _tf);
                graph.DrawString(q.TisubF9, _font, _solid, new RectangleF(600, 575, width, height), _tf);
                graph.DrawString("DATOS FINALES DE FASE:", _font, _solid, new RectangleF(20, 595, width, height), _tf);
                graph.DrawString(q.Tff9, _font, _solid, new RectangleF(300, 595, width, height), _tf);

                graph.DrawString("FASE 10: " + q.Fase10, _font, _solid, new RectangleF(20, 620, width, height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 620, width, height), _tf);
                graph.DrawString(q.DuracionTotalF10 + " " + "min.s", _font, _solid, new RectangleF(420, 620, width, height), _tf);

                graph.DrawString("FASE 11: " + q.Fase11, _font, _solid, new RectangleF(20, 645, width, height), _tf);
                graph.DrawString("DURAC.TOTAL FASE:", _font, _solid, new RectangleF(300, 645, width, height), _tf);
                graph.DrawString(q.DuracionTotalF11 + " " + "min.s", _font, _solid, new RectangleF(420, 645, width, height), _tf);

                graph.DrawString("FASE 12: FIN DE CICLO         TIEMPO TP  TE2 TE3 TE4 TE9 TE10", _font, _solid, new RectangleF(20, 670, width, height), _tf);
                var tif12 = q.Tif12.Split(new String[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                graph.DrawString(tif12[0] + " " + "<--[  ]", _negrita, _solid, new RectangleF(20, 690, width, height), _tf);
                graph.DrawString($"{tif12[1]}  {tif12[2]}  {tif12[3]}  {tif12[4]}  {tif12[5]}  {tif12[6]}", _font, _solid, new RectangleF(200, 690, width, height), _tf);

                //graph.DrawString(q.Tif12.Substring(0, 6) + " " + "<--[  ]", _negrita, _solid, new RectangleF(20, 690, width, height), _tf);
                //graph.DrawString(q.Tif12.Substring(6), _font, _solid, new RectangleF(200, 690, width, height), _tf);
                graph.DrawString(q.TisubF12.Substring(0, 2), _font, _solid, new RectangleF(600, 690, width, height), _tf);
                graph.DrawString(q.TisubF12.Substring(2) + " " + "<--[  ]", _negrita, _solid, new RectangleF(605, 690, width, height), _tf);


                graph.DrawString("HORA COMIEN.PROGR :", _font, _solid, new RectangleF(20, 735, width, height), _tf);
                graph.DrawString(q.HoraInicio, _negrita, _solid, new RectangleF(200, 735, width, height), _tf);
                graph.DrawString("HORA FIN.PROGR :", _font, _solid, new RectangleF(20, 755, width, height), _tf);
                graph.DrawString(q.HoraFin, _negrita, _solid, new RectangleF(200, 755, width, height), _tf);


                if (q.EsterilizacionN == "")
                {
                    graph.DrawString("ESTERILIZACION:", _font, _solid, new RectangleF(20, 775, width, height), _tf);
                    graph.DrawString("FALLIDA", _font, _solid, new RectangleF(200, 775, width, height), _tf);
                }
                else
                {
                    graph.DrawString("ESTERILIZACION N.:", _font, _solid, new RectangleF(20, 775, width, height), _tf);
                    graph.DrawString(q.EsterilizacionN, _font, _solid, new RectangleF(200, 775, width, height), _tf);

                }

                graph.DrawString("TEMP.MIN.ESTERILIZACION:", _font, _solid, new RectangleF(20, 795, width, height), _tf);
                graph.DrawString("°C  ", _font, _solid, new RectangleF(200, 795, width, height), _tf);
                graph.DrawString(q.Tminima + " " + "<--[  ]", _negrita, _solid, new RectangleF(220, 795, width, height), _tf);
                graph.DrawString("TEMP.MAX.ESTERILIZACION:", _font, _solid, new RectangleF(20, 815, width, height), _tf);
                graph.DrawString("°C  ", _font, _solid, new RectangleF(200, 815, width, height), _tf);
                graph.DrawString(q.Tmaxima + " " + "<--[  ]", _negrita, _solid, new RectangleF(220, 815, width, height), _tf);

                graph.DrawString("DURACION FASE DE ESTER.:", _font, _solid, new RectangleF(20, 835, width, height), _tf);
                graph.DrawString(q.DuracionTotal + " " + "min.s", _font, _solid, new RectangleF(200, 835, width, height), _tf);
                graph.DrawString("F(T,z) MIN.:", _font, _solid, new RectangleF(20, 855, width, height), _tf);
                graph.DrawString(q.FtzMin, _font, _solid, new RectangleF(200, 855, width, height), _tf);
                graph.DrawString("F(T,z) MAX.:", _font, _solid, new RectangleF(20, 875, width, height), _tf);
                graph.DrawString(q.FtzMax, _font, _solid, new RectangleF(200, 875, width, height), _tf);
                graph.DrawString("Dif F(T,z):", _font, _solid, new RectangleF(20, 895, width, height), _tf);
                graph.DrawString(q.DifMaxMin, _font, _solid, new RectangleF(200, 895, width, height), _tf);
                graph.DrawString(q.AperturaPuerta, _font, _solid, new RectangleF(20, 915, width, height), _tf);
                graph.DrawString("FIRMA OPERADOR        _______________________ ", _font, _solid, new RectangleF(20, 955, width, height), _tf);
                graph.DrawString("FIRMA GAR.DE CALID.   _______________________ ", _font, _solid, new RectangleF(20, 1015, width, height), _tf);
               
                if (q.ErrorCiclo == "")
                {
                    graph.DrawString("Pág 1 de 1  ", _font, _solid, new RectangleF(650, 1050, width, height), _tf);
                }
                else
                {
                    string[] error = q.ErrorCiclo.Split('\n');
                    if (error.Count() > 24)
                    {

                        if (page == 1)
                        {
                            graph.DrawString("ALARMAS:", _fontDos, _solid, new RectangleF(450, 740, width, height), _tf);
                            graph.DrawString("Pág 1 de 2  ", _font, _solid, new RectangleF(650, 1050, width, height), _tf);
                            e.HasMorePages = true;
                            for (int i = 0; i < error.Length; i++)
                            {
                                if (i >= 0 && i <= 24)
                                {
                                    graph.DrawString(error[i], _fontDos, _solid, new RectangleF(450, 750 + i * 10, width, height), _tf);

                                }
                            }

                        }
                        if (page == 2)
                        {
                            e.Graphics.Clear(Color.White);
                            graph.DrawString("ID. MAQUINA:" + "  " + q.IdAutoclave, _font, _solid, new RectangleF(20, 5, width, height), _tf);
                            graph.DrawString("N.PROGRESIVO:" + "  " + q.NumeroCiclo, _font, _solid, new RectangleF(190, 5, width, height), _tf);
                            graph.DrawString("Informe de ciclo de esterilización", _font, _solid, new RectangleF(360, 5, width, height), _tf);
                            graph.DrawString("Impreso: " + DateTime.Now, _font, _solid, new RectangleF(580, 5, width, height), _tf);
                            graph.DrawString("Por: " + _httpContextAccessor.HttpContext.Session.GetString("SessionName"), _font, _solid, new RectangleF(580, 20, width, height), _tf);

                            graph.DrawString("Pág 2 de 2  ", _font, _solid, new RectangleF(650, 1050, width, height), _tf);
                            e.HasMorePages = false;
                            for (int i = 0; i < error.Length; i++)
                            {

                                if (i > 24)
                                {

                                    graph.DrawString(error[i], _fontDos, _solid, new RectangleF(50, 30 + (i - 25) * 10, width, height), _tf);

                                }
                            }
                        }
                        page++;


                    }

                    else
                    {
                        graph.DrawString("ALARMAS:", _fontDos, _solid, new RectangleF(450, 735, width, height), _tf);

                        graph.DrawString(q.ErrorCiclo, _fontDos, _solid, _rect, _td);
                        graph.DrawString("Pág 1 de 1  ", _font, _solid, new RectangleF(650, 1050, width, height), _tf);
                    }

                }


            }



        }
    }
}
