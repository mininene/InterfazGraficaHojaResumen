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
using WebResumen.Models;
using WebResumen.Models.ViewModels;
using WebResumen.Services.LogRecord;
using WebResumen.Services.PrinterService;

namespace WebResumen.Controllers
{
    [Authorize(Policy = "ADTodos")]
    public class AutoClaveFController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPrinterOchoVeinte _printerOchoVeinte;
        private readonly IPrinterDosTresCuatro _printerDosTresCuatro;
        private readonly ILogRecord _log;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AutoClaveFController(AppDbContext context, IPrinterOchoVeinte printerOchoVeinte, IPrinterDosTresCuatro printerDosTresCuatro, ILogRecord log, IHttpContextAccessor httpContextAccessor )
        {
            _context = context;
            _printerOchoVeinte = printerOchoVeinte;
            _printerDosTresCuatro = printerDosTresCuatro;
            _log = log;
            _httpContextAccessor = httpContextAccessor;

        }

        // GET: AutoClaveF
        public async Task<IActionResult> Index(string nCiclo, string nPrograma, string fecha)
        {
            List<CiclosAutoclaves> _sabiUno = await _context.CiclosAutoclaves.ToListAsync();


            var query = from x in _sabiUno.Where(x => x.IdAutoclave == "NF1030F").OrderByDescending(X => X.Id).Take(50) select x;

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


            return View(query);
        }

        public async Task<JsonResult> ListAutoclaveF()
        {
            //var result=  await _context.CiclosAutoclaves.OrderByDescending(x => x.Id).ToListAsync();
            //return View(await _context.CiclosAutoclaves.OrderByDescending(x=>x.Id).ToListAsync());
            List<CiclosAutoclaves> _sabiUno = await _context.CiclosAutoclaves.ToListAsync();
            var query = from x in _sabiUno.Where(x => x.IdAutoclave == "NF1030F").OrderByDescending(X => X.Id).Take(50) select x;


            return Json(query.ToList());


        }

        public async Task<JsonResult> ListaAutoclaveF()
        {
            //var result=  await _context.CiclosAutoclaves.OrderByDescending(x => x.Id).ToListAsync();
            //return View(await _context.CiclosAutoclaves.OrderByDescending(x=>x.Id).ToListAsync());
            List<CiclosAutoclaves> _sabiUno = await _context.CiclosAutoclaves.ToListAsync();
            var query = from x in _sabiUno.Where(x => x.IdAutoclave == "NF1030F").OrderByDescending(X => X.Id).Take(1) select x;


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

            TempData["Print"] = "El Archivo ha sido Impreso";
            string EventoF = "Re-Impresión";
            _log.Write(_httpContextAccessor.HttpContext.Session.GetString("SessionFullName"), DateTime.Now, EventoF + " " + _httpContextAccessor.HttpContext.Session.GetString("AutoclaveNumeroF"), _httpContextAccessor.HttpContext.Session.GetString("SessionComentarioF"));

            return RedirectToAction("Index", "AutoClaveF");
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
            string path = @"\\essaappserver01\HojaResumen\API\AutoClaveF\" + ciclo;


            string[] texts = System.IO.File.ReadAllLines(path, new UnicodeEncoding());
            ViewBag.Data = texts;



            return View(ciclosAutoclaves);

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

            HttpContext.Session.SetString("SessionDatosF", ciclosAutoclaves.Id.ToString());
            HttpContext.Session.SetString("AutoclaveNumeroF", ("AutoClaveF" + " " + "N°Ciclo:" + ciclosAutoclaves.NumeroCiclo).ToString());

            //ViewBag.datos = ciclosAutoclaves.Id;



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

                                    HttpContext.Session.SetString("SessionFullName", fullName);
                                    HttpContext.Session.SetString("SessionPassF", model.Contraseña);
                                    HttpContext.Session.SetString("SessionNameF", model.Usuario);
                                    HttpContext.Session.SetString("SessionComentarioF", model.Comentario);
                                    //HttpContext.Session.SetString("SessionDatosF", model.Dato);
                                    HttpContext.Session.SetString("SessionTiempoF", DateTime.Now.ToString("HH:mm:ss"));
                                   
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



        // GET: AutoClaveF/Details/5
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

        // GET: AutoClaveF/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AutoClaveF/Create
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

        // GET: AutoClaveF/Edit/5
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

        // POST: AutoClaveF/Edit/5
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

        // GET: AutoClaveF/Delete/5
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

        // POST: AutoClaveF/Delete/5
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
