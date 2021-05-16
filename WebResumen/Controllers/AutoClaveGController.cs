using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReflectionIT.Mvc.Paging;
using WebResumen.Models;
using WebResumen.Models.ViewModels;
using WebResumen.Services.LogRecord;
using WebResumen.Services.PrinterService;
using WebResumen.Services.printerServiceAS;

namespace WebResumen.Controllers
{
    //[Authorize(Policy = "ADTodos")]
    public class AutoClaveGController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPrinterOchoVeinte _printerOchoVeinte;
        private readonly IPrinterDosTresCuatro _printerDosTresCuatro;
        private readonly ILogRecord _log;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPrinterOchoVeinteAS _printerOchoVeinteAS;
        private readonly IPrinterDosTresCuatroAS _printerDosTresCuatroAS;
        private readonly IConfiguration _config;
        private readonly IAuthorizationService _authorizationService;


        public AutoClaveGController(AppDbContext context, IPrinterOchoVeinte printerOchoVeinte, IPrinterDosTresCuatro printerDosTresCuatro, ILogRecord log, 
            IHttpContextAccessor httpContextAccessor, IPrinterOchoVeinteAS printerOchoVeinteAS, IPrinterDosTresCuatroAS printerDosTresCuatroAS, IConfiguration config, IAuthorizationService authorizationService)
        {
            _context = context;
            _printerOchoVeinte = printerOchoVeinte;
            _printerDosTresCuatro = printerDosTresCuatro;
            _log = log;
            _httpContextAccessor = httpContextAccessor;
            _printerOchoVeinteAS = printerOchoVeinteAS;
            _printerDosTresCuatroAS = printerDosTresCuatroAS;
            _config = config;
            _authorizationService = authorizationService;
    }

        // GET: AutoClaveG
        public async Task<IActionResult> Index(string nCiclo, string nPrograma, string fecha, int? page)
        {
            var query = _context.CiclosAutoclaves.Where(x => x.IdAutoclave == "NF1031G").AsNoTracking().AsQueryable();
           
            if (!String.IsNullOrEmpty(nCiclo))
            {
                page = 1;
                query = query.Where(x => x.NumeroCiclo.Contains(nCiclo));
              

            }

            if (!String.IsNullOrEmpty(nPrograma))
            {
                page = 1;
                query = query.Where(x => x.Programa.Contains(nPrograma));
            }



            if (!String.IsNullOrEmpty(fecha))
            {
                page = 1;
                query = query.Where(x => x.HoraFin.Contains(fecha));

            }

            if (!String.IsNullOrEmpty(nCiclo) && !String.IsNullOrEmpty(nPrograma) && !String.IsNullOrEmpty(fecha))
            {
                page = 1;
                query = query.Where(x => x.NumeroCiclo.Contains(nCiclo)
                                       || x.Programa.Contains(nPrograma)
                                         || x.HoraFin.Contains(fecha));  // si pongo la fecha como string si que lo coge
            }
           
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            var model = await PagingList.CreateAsync(query.OrderByDescending(X => X.Id), pageSize, pageNumber);
           
            return View(model);
        }

        public async Task<JsonResult> ListAutoclaveG()
        {
            //var result=  await _context.CiclosAutoclaves.OrderByDescending(x => x.Id).ToListAsync();
            //return View(await _context.CiclosAutoclaves.OrderByDescending(x=>x.Id).ToListAsync());
            List<CiclosAutoclaves> _sabiUno = await _context.CiclosAutoclaves.ToListAsync();
            var query = from x in _sabiUno.Where(x => x.IdAutoclave == "NF1031G").OrderByDescending(X => X.Id).Take(50) select x;


            return Json(query.ToList());


        }

        public async Task<JsonResult> ListaAutoclaveG()
        {
            //var result=  await _context.CiclosAutoclaves.OrderByDescending(x => x.Id).ToListAsync();
            //return View(await _context.CiclosAutoclaves.OrderByDescending(x=>x.Id).ToListAsync());
            List<CiclosAutoclaves> _sabiUno = await _context.CiclosAutoclaves.ToListAsync();
            var query = from x in _sabiUno.Where(x => x.IdAutoclave == "NF1031G").OrderByDescending(X => X.Id).Take(1) select x;


            return Json(query.ToList());


        }


        public async Task<IActionResult> Print(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ciclosAutoclaves = await _context.CiclosAutoclaves
               .FirstOrDefaultAsync(m => m.Id == id);

           // if (ciclosAutoclaves.Programa.Trim().Equals("8") || ciclosAutoclaves.Programa.Trim().Equals("20"))
                int ciclosInt = Convert.ToInt32(ciclosAutoclaves.Programa.Trim());
            if (ciclosInt >= 5)

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
            TempData["Print"] = "El Archivo ha sido Impreso";
            string EventoG = "Re-Impresión";
            _log.Write(_httpContextAccessor.HttpContext.Session.GetString("SessionFullName"), DateTime.Now, EventoG + " " + _httpContextAccessor.HttpContext.Session.GetString("AutoclaveNumeroG"), _httpContextAccessor.HttpContext.Session.GetString("SessionComentarioG"));

            return RedirectToAction("Index", "AutoClaveG");
            //return View(ciclosAutoclaves);
        }

        public async Task<IActionResult> PrintAS(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ciclosAutoclaves = await _context.CiclosAutoclaves
               .FirstOrDefaultAsync(m => m.Id == id);

           // if (ciclosAutoclaves.Programa.Trim().Equals("8") || ciclosAutoclaves.Programa.Trim().Equals("20"))
                int ciclosInt = Convert.ToInt32(ciclosAutoclaves.Programa.Trim());
            if (ciclosInt >= 5)

            {
                _printerOchoVeinteAS.printOchoVeinteAS(id);
            }

            if (ciclosAutoclaves.Programa.Trim().Equals("2") || ciclosAutoclaves.Programa.Trim().Equals("3") || ciclosAutoclaves.Programa.Trim().Equals("4"))
            {
                _printerDosTresCuatroAS.printDosTresCuatroAS(id);
            }


            if (ciclosAutoclaves == null)
            {
                return NotFound();
            }
            TempData["Print"] = "El Archivo ha sido Impreso";
           
            return View("Printing");
            //return View(ciclosAutoclaves);
        }

        public async Task<IActionResult> WritePrint()
        {
            //string ReportURL = @"\\essaappserver01\HojaResumen\old\archivo1.pdf";
            string ReportURL = _config["OptionalSettings:Pdf"] + "\\PDF\\archivo1.pdf";
            byte[] FileBytes = System.IO.File.ReadAllBytes(ReportURL);
            TempData["Print"] = "El Archivo ha sido Impreso";
            string EventoG = "Re-Impresión";
            _log.Write(_httpContextAccessor.HttpContext.Session.GetString("SessionFullName"), DateTime.Now, EventoG + " " + _httpContextAccessor.HttpContext.Session.GetString("AutoclaveNumeroG"), _httpContextAccessor.HttpContext.Session.GetString("SessionComentarioG"));
            return File(FileBytes, "application/pdf");
            //return File(new FileStream(@"\\essaappserver01\HojaResumen\old\archivo1.pdf", FileMode.Open, FileAccess.Read), "application/pdf");



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
           // string path = @"\\essaappserver01\HojaResumen\API\AutoClaveG\" + ciclo;
            var query = _context.MaestroAutoclave.Where(t => t.Matricula == "NF1031G").FirstOrDefault();
            var path = query.RutaSalida.ToString() + ciclo;


            string[] texts = System.IO.File.ReadAllLines(path, new UnicodeEncoding());
            ViewBag.Data = texts;

            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, "text/html", ciclo);

           // return View(ciclosAutoclaves);

        }

        public async Task<IActionResult> CycleList(string ciclo, int? page)
        {
            var query = _context.MaestroAutoclave.Where(t => t.Matricula == "NF1031G").FirstOrDefault();
            var path = query.RutaSalida.ToString();

            DirectoryInfo dir = new DirectoryInfo(path);
            List<DownloadViewModel> Cyclelist = new List<DownloadViewModel>();
            foreach (FileInfo file in dir.GetFiles())
            {
                Cyclelist.Add(new DownloadViewModel { Dir = file.FullName, Ciclo = file.FullName.Split('\\')[5], Numero = Convert.ToInt32(file.FullName.Split('\\')[5].Split('G')[1].Split('.')[0]) });
            }


            if (!String.IsNullOrEmpty(ciclo))
            {
                page = 1;
                Cyclelist = Cyclelist.Where(x => x.Ciclo.Contains(ciclo)).ToList();

            }
            var testq = Cyclelist.AsQueryable();
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            var model = PagingList.Create(testq.OrderByDescending(t => t.Dir), pageSize, pageNumber);
            model.Action = "CycleList";


            return View(model);

        }

        public async Task<IActionResult> Download(string dir, string ciclo)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, "AdminSupervisor");
            var authorizationResult2 = await _authorizationService.AuthorizeAsync(User, "Users");
            if (authorizationResult.Succeeded || authorizationResult2.Succeeded)
            {
                var path = $"https://essahojaresumen.global.baxter.com/LOGFiles/AutoClaveG/{ciclo}";
                using (WebClient wc = new WebClient())
                {
                    var byteArr = wc.DownloadData(path);
                    return File(byteArr, "text/html", ciclo);
                }
            } //else
            {
                return Redirect("/Inicio");
            }

            //var authorizationResult =  await _authorizationService.AuthorizeAsync(User, "AdminSupervisor");
            //var authorizationResult2 = await _authorizationService.AuthorizeAsync(User, "Users");
            //if (authorizationResult.Succeeded || authorizationResult2.Succeeded)
            //{ 
            //    byte[] fileBytes = System.IO.File.ReadAllBytes(dir);
            //    return File(fileBytes, "text/html", ciclo);
            //}
            //else
            //{
            //    return Redirect("/Inicio");
            //}

        }


        ///////////////////////////////////////////////////////////////
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

           // ViewBag.datos = ciclosAutoclaves.Id;
            HttpContext.Session.SetString("SessionDatosG", ciclosAutoclaves.Id.ToString());
            HttpContext.Session.SetString("AutoclaveNumeroG", ("AutoClaveG" + " " + "N°Ciclo:" + ciclosAutoclaves.NumeroCiclo).ToString());



            return View("Login");

        }

        ///////////////////////////////////////////////////////////////
        [HttpPost]
        public IActionResult Login(DoubleLoginViewModel model, int? id)
        {
            var ciclosAutoclaves = _context.CiclosAutoclaves
               .FirstOrDefaultAsync(m => m.Id == id);

            if (ModelState.IsValid)
            {

                string dominio = _config["SecuritySettings:Dominio"];
                string path = _config["SecuritySettings:ADPath"];
                using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, dominio, model.Usuario, model.Contraseña))
                {


                    try
                    {
                        UserPrincipal user = UserPrincipal.FindByIdentity(ctx, model.Usuario);

                        GroupPrincipal groupAdmins = GroupPrincipal.FindByIdentity(ctx, _config["SecuritySettings:ADGroupAdmins"]);
                        GroupPrincipal groupSupervisors = GroupPrincipal.FindByIdentity(ctx, _config["SecuritySettings:ADGroupSupervisors"]);
                        GroupPrincipal groupUsers = GroupPrincipal.FindByIdentity(ctx, _config["SecuritySettings:ADGroupUsers"]);


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

                                    HttpContext.Session.SetString("SessionFullName", fullName);
                                    HttpContext.Session.SetString("SessionPassG", model.Contraseña);
                                    HttpContext.Session.SetString("SessionNameG", model.Usuario);
                                    HttpContext.Session.SetString("SessionComentarioG", model.Comentario);
                                   // HttpContext.Session.SetString("SessionDatosG", model.Dato);
                                    HttpContext.Session.SetString("SessionTiempoG", DateTime.Now.ToString("HH:mm:ss"));
                                   
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


        // GET: AutoClaveG/Details/5
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

        // GET: AutoClaveG/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AutoClaveG/Create
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

        // GET: AutoClaveG/Edit/5
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

        // POST: AutoClaveG/Edit/5
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

        // GET: AutoClaveG/Delete/5
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

        // POST: AutoClaveG/Delete/5
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
