using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebResumen.Models;
using WebResumen.Models.ViewModels;
using WebResumen.Services.LogRecord;
using WebResumen.Services.PrinterService;
using WebResumen.Services.printerServiceAS;

namespace WebResumen.Controllers
{
   // [Authorize(Policy = "ADTodos")]
    public class AutoClaveAController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPrinterOchoVeinte _printerOchoVeinte;
        private readonly IPrinterOchoVeinteAS _printerOchoVeinteAS;
        private readonly IPrinterDosTresCuatro _printerDosTresCuatro;
        private readonly IPrinterDosTresCuatroAS _printerDosTresCuatroAS;
        private readonly ILogRecord _log;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AutoClaveAController(AppDbContext context, IPrinterOchoVeinte printerOchoVeinte, IPrinterDosTresCuatro printerDosTresCuatro, ILogRecord log, IHttpContextAccessor httpContextAccessor, IPrinterOchoVeinteAS printerOchoVeinteAS, IPrinterDosTresCuatroAS printerDosTresCuatroAS)
        {
            _context = context;
            _printerOchoVeinte = printerOchoVeinte;
            _printerDosTresCuatro = printerDosTresCuatro;
            _log= log;
            _httpContextAccessor = httpContextAccessor;
            _printerOchoVeinteAS = printerOchoVeinteAS;
            _printerDosTresCuatroAS = printerDosTresCuatroAS;
        }

        // GET: AutoClaveA
        public async Task<IActionResult> Index(string nCiclo, string nPrograma, string fecha)
        {
            List<ViewModelAutoClaveJ> _autoJ = new List<ViewModelAutoClaveJ>();
            List<CiclosAutoclaves> _sabiUno = await _context.CiclosAutoclaves.ToListAsync();



            var query = from x in _sabiUno.Where(x => x.IdAutoclave == "NF8387A").OrderByDescending(X => X.Id).Take(50) select x;
           
            if (!String.IsNullOrEmpty(nCiclo))
            {
                query = query.Where(x => x.NumeroCiclo.Contains(nCiclo));
                                      
            }

            if (!String.IsNullOrEmpty(nPrograma))
            {
                query = query.Where(x => x.Programa.Contains(nPrograma));
            }

     

            if (!String.IsNullOrEmpty(fecha))
            {
                query = query.Where(x => x.HoraFin.Contains(fecha));
                                   
            }

            if (!String.IsNullOrEmpty(nCiclo) && !String.IsNullOrEmpty(nPrograma) && !String.IsNullOrEmpty(fecha))
            {
                query = query.Where(x => x.NumeroCiclo.Contains(nCiclo)
                                       || x.Programa.Contains(nPrograma)
                                         || x.HoraFin.Contains(fecha));  // si pongo la fecha como string si que lo coge
            }
            DateTime expiry = Convert.ToDateTime(_httpContextAccessor.HttpContext.Session.GetString("SessionTiempo"));
            ViewBag.expiry = expiry;
            if (expiry <= DateTime.Now)
            {
                string EventoI = "Cierre de sesión";
                string ComentarioI = "Ha Cerrado sesión";
                string usuario = _httpContextAccessor.HttpContext.Session.GetString("SessionName");
                _log.Write(usuario, DateTime.Now, EventoI, ComentarioI);
                return RedirectToAction("Logout", "Home");
            }
                return View(query);
        }



        public async Task<JsonResult> ListAutoclaveA()
        {

            List<CiclosAutoclaves> _sabiUno = await _context.CiclosAutoclaves.ToListAsync();
            var query = from x in _sabiUno.Where(x => x.IdAutoclave == "NF8387A").OrderByDescending(X => X.Id).Take(50) select x;


            return Json(query.ToList());




        }


        public class PostModel
        {
            public string nCiclo { get; set; }
            public string nPrograma { get; set; }
            public string fecha { get; set; }
        }


        [HttpPost]
      
        public async Task<JsonResult> ListAutoclaveA([FromBody]  PostModel model)
        {
            //var result=  await _context.CiclosAutoclaves.OrderByDescending(x => x.Id).ToListAsync();
            //return View(await _context.CiclosAutoclaves.OrderByDescending(x=>x.Id).ToListAsync());
            //List<CiclosAutoclaves> _sabiUno = await _context.CiclosAutoclaves.ToListAsync();
            //var query = from x in _sabiUno.Where(x => x.IdAutoclave == "NF8387A").OrderByDescending(X => X.Id).Take(50) select x;
            string vb = model.nCiclo;
            string hf = model.nPrograma;

            //return Json( query.ToList());
            List<ViewModelAutoClaveJ> _autoJ = new List<ViewModelAutoClaveJ>();
            List<CiclosAutoclaves> _sabiUno = await _context.CiclosAutoclaves.ToListAsync();
            var query = from x in _sabiUno.Where(x => x.IdAutoclave == "NF8387A").OrderByDescending(X => X.Id).Take(50) select x;

            if (!String.IsNullOrEmpty(model.nCiclo))
            {
                query = query.Where(x => x.NumeroCiclo.Contains(model.nCiclo));

            }

            if (!String.IsNullOrEmpty(model.nPrograma))
            {
                query = query.Where(x => x.Programa.Contains(model.nPrograma));
            }



            if (!String.IsNullOrEmpty(model.fecha))
            {
                query = query.Where(x => x.HoraFin.Contains(model.fecha));

            }

            if (!String.IsNullOrEmpty(model.nCiclo) && !String.IsNullOrEmpty(model.nPrograma) && !String.IsNullOrEmpty(model.fecha))
            {
                query = query.Where(x => x.NumeroCiclo.Contains(model.nCiclo)
                                       || x.Programa.Contains(model.nPrograma)
                                         || x.HoraFin.Contains(model.fecha));  // si pongo la fecha como string si que lo coge
            }
            return Json( query.ToList());


        }

        public async Task<JsonResult> ListaAutoclaveA()
        {
            //var result=  await _context.CiclosAutoclaves.OrderByDescending(x => x.Id).ToListAsync();
            //return View(await _context.CiclosAutoclaves.OrderByDescending(x=>x.Id).ToListAsync());
            List<CiclosAutoclaves> _sabiUno = await _context.CiclosAutoclaves.ToListAsync();

            //var query = from x in _sabiUno.Where(x => x.IdAutoclave == "NF8387A").OrderByDescending(X => X.Id).Take(1) select x;
            var querya = from x in _sabiUno.Where(x => x.IdAutoclave == "NF8387A").OrderByDescending(x => x.Id).Take(1) select x;
            var queryb = from x in _sabiUno.Where(x => x.IdAutoclave == "8388B").OrderByDescending(x => x.Id).Take(1) select x;
            var queryc = from x in _sabiUno.Where(x => x.IdAutoclave == "8389C").OrderByDescending(x => x.Id).Take(1) select x;
            var queryd = from x in _sabiUno.Where(x => x.IdAutoclave == "8607D").OrderByDescending(x => x.Id).Take(1) select x;
            var querye = from x in _sabiUno.Where(x => x.IdAutoclave == "NF1029E").OrderByDescending(x => x.Id).Take(1) select x;
            var queryf = from x in _sabiUno.Where(x => x.IdAutoclave == "NF1030F").OrderByDescending(x => x.Id).Take(1) select x;
            var queryg = from x in _sabiUno.Where(x => x.IdAutoclave == "NF1031G").OrderByDescending(x => x.Id).Take(1) select x;
            var queryh = from x in _sabiUno.Where(x => x.IdAutoclave == "NA0658EGH").OrderByDescending(x => x.Id).Take(1) select x;
            var queryi = from x in _sabiUno.Where(x => x.IdAutoclave == "NA0672EGI").OrderByDescending(x => x.Id).Take(1) select x;
            var querym = from x in _sabiUno.Where(x => x.IdAutoclave == "NA0611EFM").OrderByDescending(x => x.Id).Take(1) select x;
          
            var pc = querya.Union(queryb).Union(queryc).Union(queryd).Union(querye).Union(queryf).Union(queryg).Union(queryh)
                .Union(queryi).Union(querym);

            //return Json(query.ToList());

            return Json(pc);


        }

        public async Task<IActionResult> Preview(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciclosAutoclaves = await _context.CiclosAutoclaves
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ciclosAutoclaves == null)
            {
                return NotFound();
            }

            int numero = int.Parse(ciclosAutoclaves.NumeroCiclo);

            string ciclo = ciclosAutoclaves.IdAutoclave + string.Format("{0:00000}", numero) + ".LOG";
            string path = @"\\essaappserver01\HojaResumen\API\AutoClaveA\" + ciclo;


            string[] texts = System.IO.File.ReadAllLines(path, new UnicodeEncoding());
            ViewBag.Data = texts;



            return View(ciclosAutoclaves);

        }

        public async Task<IActionResult> Print(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ciclosAutoclaves = await _context.CiclosAutoclaves
               .FirstOrDefaultAsync(m => m.Id == id);

            if (ciclosAutoclaves.Programa.Trim().Equals("8") || ciclosAutoclaves.Programa.Trim().Equals("20"))

            {
                _printerOchoVeinte.printOchoVeinte(id);
            }

            if (ciclosAutoclaves.Programa.Trim().Equals("2") || ciclosAutoclaves.Programa.Trim().Equals("3") || ciclosAutoclaves.Programa.Trim().Equals("4"))
            {
                _printerDosTresCuatro.printDosTresCuatro(id);
            }


            if (ciclosAutoclaves == null)
            {
                return NotFound();
            }
            string EventoA = "Re-Impresión";
            TempData["Print"] = "El Archivo ha sido Impreso";
            _log.Write(_httpContextAccessor.HttpContext.Session.GetString("SessionFullName"), DateTime.Now, EventoA + " " + _httpContextAccessor.HttpContext.Session.GetString("AutoclaveNumeroA"), _httpContextAccessor.HttpContext.Session.GetString("SessionComentarioA"));

            return RedirectToAction("Index", "AutoClaveA");




        }
       

        public async Task<IActionResult> PrintAS(int? id)
        {
            

            if (id == null)
            {
                return NotFound();
            }
            var ciclosAutoclaves = await _context.CiclosAutoclaves
               .FirstOrDefaultAsync(m => m.Id == id);

            if (ciclosAutoclaves.Programa.Trim().Equals("8") || ciclosAutoclaves.Programa.Trim().Equals("20"))

            {
                _printerOchoVeinteAS.printOchoVeinteAS(id);
               
                //return File(new FileStream(@"\\essaappserver01\HojaResumen\old\archivo1.pdf", FileMode.Open, FileAccess.Read), "application/pdf");
                // return File(System.IO.File.ReadAllBytes("archivo.pdf"), "application/pdf");

                //MemoryStream ms = new MemoryStream();
                //Document document = new Document(iTextSharp.text.PageSize.Letter, 0, 0, 0, 0);
                //PdfWriter pw = PdfWriter.GetInstance(document, ms);
                //document.Open();
                //document.Add(new Paragraph("Hola Mundo"));
                //document.Close();
                //byte[] bytesStream = ms.ToArray();

                //ms = new MemoryStream();
                //ms.Write(bytesStream, 0, bytesStream.Length);
                //ms.Position = 0;
                //return new FileStreamResult(ms, "application/pdf");
           

            }

            if (ciclosAutoclaves.Programa.Trim().Equals("2") || ciclosAutoclaves.Programa.Trim().Equals("3") || ciclosAutoclaves.Programa.Trim().Equals("4"))
            {
                _printerDosTresCuatroAS.printDosTresCuatroAS(id);
            }


            if (ciclosAutoclaves == null)
            {
                return NotFound();
            }
            //string EventoA = "Re-Impresión";
            TempData["Print"] = "El Archivo ha sido Generado";
            //_log.Write(_httpContextAccessor.HttpContext.Session.GetString("SessionFullName"), DateTime.Now, EventoA + " " + _httpContextAccessor.HttpContext.Session.GetString("AutoclaveNumeroA"), _httpContextAccessor.HttpContext.Session.GetString("SessionComentarioA"));

            return View("Printing");



        }
        public async Task<IActionResult> WritePrint()
        {
            string ReportURL = @"\\essaappserver01\HojaResumen\old\archivo1.pdf";
            byte[] FileBytes = System.IO.File.ReadAllBytes(ReportURL);
            string EventoA = "Re-Impresión";
            _log.Write(_httpContextAccessor.HttpContext.Session.GetString("SessionFullName"), DateTime.Now, EventoA + " " + _httpContextAccessor.HttpContext.Session.GetString("AutoclaveNumeroA"), _httpContextAccessor.HttpContext.Session.GetString("SessionComentarioA"));
            TempData["Print"] = "El Archivo ha sido Generado";
            return File(FileBytes, "application/pdf");
            //return File(new FileStream(@"\\essaappserver01\HojaResumen\old\archivo1.pdf", FileMode.Open, FileAccess.Read), "application/pdf");

        

        }






        //JSON/////////////////////////////////////////////////////////////////////////
        //public async Task<JsonResult> Vista(int? id)
        //{
        //    if (id == null)
        //    {
        //        return Json("No existe");
        //    }

        //    var ciclosAutoclaves = await _context.CiclosAutoclaves
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //     int numero = int.Parse(ciclosAutoclaves.NumeroCiclo);

        //     string ciclo = ciclosAutoclaves.IdAutoclave + string.Format("{0:00000}", numero) + ".LOG";

        //    string path = @"\\essaappserver01\HojaResumen\API\AutoClaveA\" + ciclo;


        //    string[] texts = System.IO.File.ReadAllLines(path, new UnicodeEncoding());
        //    ViewBag.Data = texts;

        //    if (ciclosAutoclaves == null)
        //    {
        //        return Json("No existe");
        //    }


        //    return Json( ViewBag.Data);
        //}



        //public async Task<JsonResult> Print(int? id)
        //{
        //    if (id == null)
        //    {
        //        return Json("No existe");
        //    }
        //    var ciclosAutoclaves = await _context.CiclosAutoclaves
        //       .FirstOrDefaultAsync(m => m.Id == id);

        //    if (ciclosAutoclaves.Programa.Trim().Equals("8") || ciclosAutoclaves.Programa.Trim().Equals("20"))

        //    {
        //        _printerOchoVeinte.printOchoVeinte(id);
        //    }

        //    if (ciclosAutoclaves.Programa.Trim().Equals("2") || ciclosAutoclaves.Programa.Trim().Equals("3") || ciclosAutoclaves.Programa.Trim().Equals("4"))
        //    {
        //        _printerDosTresCuatro.printDosTresCuatro(id);
        //    }


        //    if (ciclosAutoclaves == null)
        //    {
        //        return Json("No existe");
        //    }


        //    return Json(ciclosAutoclaves);
        //}
        ////////////// JSON/////////////////////////////////////////////////////////

        // /////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        public async Task<IActionResult> Login(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciclosAutoclaves = await _context.CiclosAutoclaves
                .FirstOrDefaultAsync(m => m.Id == id);


            if (ciclosAutoclaves == null)
            {
                return NotFound();
            }

            //ViewBag.datos = ciclosAutoclaves.Id;
            HttpContext.Session.SetString("SessionDatosA", ciclosAutoclaves.Id.ToString());
            HttpContext.Session.SetString("AutoclaveNumeroA", ("AutoClaveA"+" "+"N°Ciclo:"+ciclosAutoclaves.NumeroCiclo).ToString());


            return View("Login");

        }

        [HttpPost]
        public IActionResult Login(DoubleLoginViewModel model, int? id)
        {
            var ciclosAutoclaves = _context.CiclosAutoclaves
               .FirstOrDefaultAsync(m => m.Id == id);

            if (ModelState.IsValid)
            {

                string dominio = @"global.baxter.com";
                string path = @"LDAP://global.baxter.com";
                using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, dominio, model.Usuario, model.Contraseña))
                {


                    try
                    {
                        UserPrincipal user = UserPrincipal.FindByIdentity(ctx, model.Usuario);

                        GroupPrincipal groupAdmins = GroupPrincipal.FindByIdentity(ctx, "GLOBAL\\ESSA-HojaResumen_Admins");
                        GroupPrincipal groupSupervisors = GroupPrincipal.FindByIdentity(ctx, "GLOBAL\\ESSA-HojaResumen_Supervisors");
                        GroupPrincipal groupUsers = GroupPrincipal.FindByIdentity(ctx, "GLOBAL\\ESSA-HojaResumen_Users");

                        if (user != null)
                        {
                            if (user.IsMemberOf(groupAdmins) || user.IsMemberOf(groupSupervisors) || user.IsMemberOf(groupUsers))
                            {
                                using (var searcher = new DirectorySearcher(new DirectoryEntry(path)))
                                {
                                    string fullName = string.Empty;
                                    DirectoryEntry de = (user.GetUnderlyingObject() as DirectoryEntry);
                                    if (de != null)
                                    { fullName = de.Properties["displayName"][0].ToString(); }

                                    HttpContext.Session.SetString("SessionFullName",fullName);
                                    HttpContext.Session.SetString("SessionPassA", model.Contraseña);
                                    HttpContext.Session.SetString("SessionNameA", model.Usuario);
                                    HttpContext.Session.SetString("SessionComentarioA", model.Comentario);
                                   // HttpContext.Session.SetString("SessionDatosA", model.Dato);
                                    HttpContext.Session.SetString("SessionTiempoA", DateTime.Now.ToString("HH:mm:ss"));
                                    // string EventoA = "Re-Impresión";
                                    // _log.Write(fullName, DateTime.Now, EventoA+" "+_httpContextAccessor.HttpContext.Session.GetString("AutoclaveNumeroA"), model.Comentario);
                                    List<string> lista = new List<string>();
                                    foreach (String printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                                    {

                                       lista.Add(printer);
                                      

                                    }
                                    ViewBag.impresoras = lista.ToList();

                                    return View("Print");
                                }

                            }
                            else
                            {
                               
                                 return RedirectToAction("Logout", "Home");

                            }
                        }


                    }
                    catch
                    {


                        TempData["Fail"] = "Login Fallido. Usuario o Contraseña Incorrecta";
                        return View("Login");
                    }
                }
            }

      
            return View();


        }


        /////////////////////////////////////////////////////
























        // GET: AutoClaveA/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciclosAutoclaves = await _context.CiclosAutoclaves
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ciclosAutoclaves == null)
            {
                return NotFound();
            }

            return View(ciclosAutoclaves);
        }

        // GET: AutoClaveA/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AutoClaveA/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdAutoclave,IdSeccion,Tinicio,NumeroCiclo,Programa,Modelo,Programador,Operador,CodigoProducto,Lote,Notas,IdUsuario,Fase1,DuracionTotalF1,Fase2,DuracionTotalF2,Fase3,DuracionTotalF3,Fase4,DuracionTotalF4,Fase5,DuracionTotalF5,Tif5,TisubF5,Tff5,TfsubF5,Fase6,DuracionTotalF6,Tif6,TisubF6,Tff6,TfsubF6,Fase7,DuracionTotalF7,Tif7,TisubF7,Fase8,DuracionTotalF8,Tif8,TisubF8,Tff8,Fase9,DuracionTotalF9,Tif9,TisubF9,Tff9,Fase10,DuracionTotalF10,Fase11,DuracionTotalF11,Fase12,DuracionTotalF12,Fase13,Tff13,TfsubF13,HoraInicio,HoraFin,EsterilizacionN,Tminima,Tmaxima,DuracionTotal,FtzMin,FtzMax,DifMaxMin,AperturaPuerta,TiempoCiclo,ErrorCiclo,FechaRegistro")] CiclosAutoclaves ciclosAutoclaves)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ciclosAutoclaves);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ciclosAutoclaves);
        }

        // GET: AutoClaveA/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciclosAutoclaves = await _context.CiclosAutoclaves.FindAsync(id);
            if (ciclosAutoclaves == null)
            {
                return NotFound();
            }
            return View(ciclosAutoclaves);
        }

        // POST: AutoClaveA/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdAutoclave,IdSeccion,Tinicio,NumeroCiclo,Programa,Modelo,Programador,Operador,CodigoProducto,Lote,Notas,IdUsuario,Fase1,DuracionTotalF1,Fase2,DuracionTotalF2,Fase3,DuracionTotalF3,Fase4,DuracionTotalF4,Fase5,DuracionTotalF5,Tif5,TisubF5,Tff5,TfsubF5,Fase6,DuracionTotalF6,Tif6,TisubF6,Tff6,TfsubF6,Fase7,DuracionTotalF7,Tif7,TisubF7,Fase8,DuracionTotalF8,Tif8,TisubF8,Tff8,Fase9,DuracionTotalF9,Tif9,TisubF9,Tff9,Fase10,DuracionTotalF10,Fase11,DuracionTotalF11,Fase12,DuracionTotalF12,Fase13,Tff13,TfsubF13,HoraInicio,HoraFin,EsterilizacionN,Tminima,Tmaxima,DuracionTotal,FtzMin,FtzMax,DifMaxMin,AperturaPuerta,TiempoCiclo,ErrorCiclo,FechaRegistro")] CiclosAutoclaves ciclosAutoclaves)
        {
            if (id != ciclosAutoclaves.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ciclosAutoclaves);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CiclosAutoclavesExists(ciclosAutoclaves.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ciclosAutoclaves);
        }

        // GET: AutoClaveA/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciclosAutoclaves = await _context.CiclosAutoclaves
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ciclosAutoclaves == null)
            {
                return NotFound();
            }

            return View(ciclosAutoclaves);
        }

        // POST: AutoClaveA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ciclosAutoclaves = await _context.CiclosAutoclaves.FindAsync(id);
            _context.CiclosAutoclaves.Remove(ciclosAutoclaves);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CiclosAutoclavesExists(int id)
        {
            return _context.CiclosAutoclaves.Any(e => e.Id == id);
        }
    }
}
