using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
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
    
    public class AutoClaveKController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPrinterNueveDiez _printerNueveDiez;
        private readonly ILogRecord _log;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPrinterNueveDiezAS _printerNueveDiezAS;
        private readonly IConfiguration _config;

        public AutoClaveKController(AppDbContext context, IPrinterNueveDiez printerNueveDiez, ILogRecord log, IHttpContextAccessor httpContextAccessor, 
            IPrinterNueveDiezAS printerNueveDiezAS, IConfiguration config)
        {
            _context = context;
            _printerNueveDiez = printerNueveDiez;
            _log=log;
            _httpContextAccessor = httpContextAccessor;
            _printerNueveDiezAS = printerNueveDiezAS;
            _config = config;
        }

        // GET: AutoClaveK
        public async Task<IActionResult> Index(string nCiclo, string nPrograma, string fecha, int? page)
        {
           
            var query = _context.CiclosSabiDos.Where(x => x.IdAutoclave == "0828K").AsNoTracking().AsQueryable();
                                  

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


        public async Task<JsonResult> ListAutoclaveK()
        {
            //var result=  await _context.CiclosAutoclaves.OrderByDescending(x => x.Id).ToListAsync();
            //return View(await _context.CiclosAutoclaves.OrderByDescending(x=>x.Id).ToListAsync());
            List<CiclosSabiDos> _sabiDos = await _context.CiclosSabiDos.ToListAsync();
            var query = from x in _sabiDos.Where(x => x.IdAutoclave == "0828K").OrderByDescending(X => X.Id).Take(50) select x;


            return Json(query.ToList());


        }

        public async Task<JsonResult> ListaAutoclaveK()
        {
            //var result=  await _context.CiclosAutoclaves.OrderByDescending(x => x.Id).ToListAsync();
            //return View(await _context.CiclosAutoclaves.OrderByDescending(x=>x.Id).ToListAsync());
            List<CiclosSabiDos> _sabiDos = await _context.CiclosSabiDos.ToListAsync();
            var query = from x in _sabiDos.Where(x => x.IdAutoclave == "0828K").OrderByDescending(X => X.Id).Take(1) select x;


            return Json(query.ToList());


        }


        public async Task<IActionResult> Print(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ciclosAutoclaves = await _context.CiclosSabiDos
               .FirstOrDefaultAsync(m => m.Id == id);

            int ciclosInt = Convert.ToInt32(ciclosAutoclaves.Programa.Trim());
            if (ciclosInt > 0)

              //  if (ciclosAutoclaves.Programa.Trim().Equals("9") || ciclosAutoclaves.Programa.Trim().Equals("10"))

            {
                _printerNueveDiez.printNueveDiez(id);
            }


            if (ciclosAutoclaves == null)
            {
                return NotFound();
            }
            TempData["Print"] = "El Archivo ha sido Impreso";
            string EventoK = "Re-Impresión";
            _log.Write(_httpContextAccessor.HttpContext.Session.GetString("SessionFullName"), DateTime.Now, EventoK + " " + _httpContextAccessor.HttpContext.Session.GetString("AutoclaveNumeroK"), _httpContextAccessor.HttpContext.Session.GetString("SessionComentarioK"));

            return RedirectToAction("Index", "AutoClaveK");
            //return View(ciclosAutoclaves);
        }

        public async Task<IActionResult> PrintAS(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ciclosAutoclaves = await _context.CiclosSabiDos
               .FirstOrDefaultAsync(m => m.Id == id);

           // if (ciclosAutoclaves.Programa.Trim().Equals("9") || ciclosAutoclaves.Programa.Trim().Equals("10"))
                int ciclosInt = Convert.ToInt32(ciclosAutoclaves.Programa.Trim());
            if (ciclosInt > 0)

            {
                _printerNueveDiezAS.printNueveDiezAS(id);
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
            string ReportURL = _config["OptionalSettings:Pdf"] + "\\PDF\\archivo2.pdf";
            byte[] FileBytes = System.IO.File.ReadAllBytes(ReportURL);
            TempData["Print"] = "El Archivo ha sido Impreso";
            string EventoK = "Re-Impresión";
            _log.Write(_httpContextAccessor.HttpContext.Session.GetString("SessionFullName"), DateTime.Now, EventoK + " " + _httpContextAccessor.HttpContext.Session.GetString("AutoclaveNumeroK"), _httpContextAccessor.HttpContext.Session.GetString("SessionComentarioK"));
            return File(FileBytes, "application/pdf");
            //return File(new FileStream(@"\\essaappserver01\HojaResumen\old\archivo1.pdf", FileMode.Open, FileAccess.Read), "application/pdf");



        }


        public async Task<IActionResult> Preview(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciclosAutoclaves = await _context.CiclosSabiDos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ciclosAutoclaves == null)
            {
                return NotFound();
            }

            int numero = int.Parse(ciclosAutoclaves.NumeroCiclo);

            string ciclo = ciclosAutoclaves.IdAutoclave + string.Format("{0:00000}", numero) + ".LOG";
            //string path = @"\\essaappserver01\HojaResumen\API\AutoClaveK\" + ciclo;
            var query = _context.MaestroAutoclave.Where(t => t.Matricula == "0828K").FirstOrDefault();
            var path = query.RutaSalida.ToString() + ciclo;

            string[] texts = System.IO.File.ReadAllLines(path, new UnicodeEncoding());
            ViewBag.Data = texts;

            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, "text/html", ciclo);

           // return View(ciclosAutoclaves);

        }
        ///////////////////////////////////////////////////////////////
        [HttpGet]
        public async Task<IActionResult> Login(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciclosAutoclaves = await _context.CiclosSabiDos
                .FirstOrDefaultAsync(m => m.Id == id);


            if (ciclosAutoclaves == null)
            {
                return NotFound();
            }

            //ViewBag.datos = ciclosAutoclaves.Id;
            HttpContext.Session.SetString("SessionDatosK", ciclosAutoclaves.Id.ToString());
            HttpContext.Session.SetString("AutoclaveNumeroK", ("AutoClaveK" + " " + "N°Ciclo:" + ciclosAutoclaves.NumeroCiclo).ToString());



            return View("Login");

        }

        ///////////////////////////////////////////////////////////////
        [HttpPost]
        public IActionResult Login(DoubleLoginViewModel model, int? id)
        {
            var ciclosAutoclaves = _context.CiclosSabiDos
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
                                    HttpContext.Session.SetString("SessionPassK", model.Contraseña);
                                    HttpContext.Session.SetString("SessionNameK", model.Usuario);
                                    HttpContext.Session.SetString("SessionComentarioK", model.Comentario);
                                    //HttpContext.Session.SetString("SessionDatosK", model.Dato);
                                    HttpContext.Session.SetString("SessionTiempoK", DateTime.Now.ToString("HH:mm:ss"));
                                   
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







        // GET: AutoClaveK/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciclosSabiDos = await _context.CiclosSabiDos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ciclosSabiDos == null)
            {
                return NotFound();
            }

            return View(ciclosSabiDos);
        }

        // GET: AutoClaveK/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AutoClaveK/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdAutoclave,IdSeccion,Tinicio,NumeroCiclo,Programa,Modelo,Programador,Operador,CodigoProducto,Lote,Notas,IdUsuario,Fase1,DuracionTotalF1,Fase2,DuracionTotalF2,Tif2,TisubF2,Tff2,TfsubF2,Fase3,DuracionTotalF3,Tif3,TisubF3,Tff3,TfsubF3,Fase4,DuracionTotalF4,Tif4,TisubF4,Fase5,DuracionTotalF5,Fase6,DuracionTotalF6,Fase7A,DuracionTotalF7a,Fase8A,DuracionTotalF8a,Fase7B,DuracionTotalF7b,Fase8B,DuracionTotalF8b,Fase9,DuracionTotalF9,Tif9,TisubF9,Tff9,Fase10,DuracionTotalF10,Fase11,DuracionTotalF11,Fase12,Tif12,TisubF12,HoraInicio,HoraFin,EsterilizacionN,Tminima,Tmaxima,DuracionTotal,FtzMin,FtzMax,DifMaxMin,AperturaPuerta,TiempoCiclo,ErrorCiclo,FechaRegistro")] CiclosSabiDos ciclosSabiDos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ciclosSabiDos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ciclosSabiDos);
        }

        // GET: AutoClaveK/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciclosSabiDos = await _context.CiclosSabiDos.FindAsync(id);
            if (ciclosSabiDos == null)
            {
                return NotFound();
            }
            return View(ciclosSabiDos);
        }

        // POST: AutoClaveK/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdAutoclave,IdSeccion,Tinicio,NumeroCiclo,Programa,Modelo,Programador,Operador,CodigoProducto,Lote,Notas,IdUsuario,Fase1,DuracionTotalF1,Fase2,DuracionTotalF2,Tif2,TisubF2,Tff2,TfsubF2,Fase3,DuracionTotalF3,Tif3,TisubF3,Tff3,TfsubF3,Fase4,DuracionTotalF4,Tif4,TisubF4,Fase5,DuracionTotalF5,Fase6,DuracionTotalF6,Fase7A,DuracionTotalF7a,Fase8A,DuracionTotalF8a,Fase7B,DuracionTotalF7b,Fase8B,DuracionTotalF8b,Fase9,DuracionTotalF9,Tif9,TisubF9,Tff9,Fase10,DuracionTotalF10,Fase11,DuracionTotalF11,Fase12,Tif12,TisubF12,HoraInicio,HoraFin,EsterilizacionN,Tminima,Tmaxima,DuracionTotal,FtzMin,FtzMax,DifMaxMin,AperturaPuerta,TiempoCiclo,ErrorCiclo,FechaRegistro")] CiclosSabiDos ciclosSabiDos)
        {
            if (id != ciclosSabiDos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ciclosSabiDos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CiclosSabiDosExists(ciclosSabiDos.Id))
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
            return View(ciclosSabiDos);
        }

        // GET: AutoClaveK/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciclosSabiDos = await _context.CiclosSabiDos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ciclosSabiDos == null)
            {
                return NotFound();
            }

            return View(ciclosSabiDos);
        }

        // POST: AutoClaveK/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ciclosSabiDos = await _context.CiclosSabiDos.FindAsync(id);
            _context.CiclosSabiDos.Remove(ciclosSabiDos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CiclosSabiDosExists(int id)
        {
            return _context.CiclosSabiDos.Any(e => e.Id == id);
        }
    }
}
