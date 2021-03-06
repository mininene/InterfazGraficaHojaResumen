﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebResumen.Models;
using WebResumen.Models.ViewModels;
using WebResumen.Services.LogRecord;
using WebResumen.Services.PrinterService;
using WebResumen.Services.printerServiceAS;

namespace WebResumen.Controllers
{
    // [Authorize(Policy = "ADTodos")]


    public class AutoClaveJController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPrinterNueveDiez _printerNueveDiez;
        private readonly ILogRecord _log;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPrinterNueveDiezAS _printerNueveDiezAS;
        private readonly IConfiguration _config;
        private readonly IAuthorizationService _authorizationService;

        public AutoClaveJController(AppDbContext context, IPrinterNueveDiez printerNueveDiez, ILogRecord log, IHttpContextAccessor httpContextAccessor,
            IPrinterNueveDiezAS printerNueveDiezAS, IConfiguration config, IAuthorizationService authorizationService)
        {
            _context = context;
            _printerNueveDiez = printerNueveDiez;
            _log = log;
            _httpContextAccessor = httpContextAccessor;
            _printerNueveDiezAS = printerNueveDiezAS;
            _config = config;
            _authorizationService = authorizationService;
        }

        // GET: AutoClaveJJ
        public async Task<IActionResult> Index(string nCiclo, string nPrograma, string fecha, int? page)
        {

            var query = _context.CiclosSabiDos.Where(x => x.IdAutoclave == "0827J").AsNoTracking().AsQueryable();


            if (!String.IsNullOrEmpty(nCiclo))
            {
               
                query = query.Where(x => x.NumeroCiclo.Contains(nCiclo));

            }

            if (!String.IsNullOrEmpty(nPrograma))
            {
                
                query = query.Where(x => x.Programa.Equals(nPrograma));
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

            int pageNumber = (page ?? 1);
            int pageSize = 50;
            int count = query.ToList().Count;
            if (pageNumber - 1 > count / pageSize)
            {
                pageNumber = 1;
            }
            var model = await PagingList.CreateAsync(query.OrderByDescending(X => X.Id), pageSize, pageNumber);
            model.RouteValue = new RouteValueDictionary {
             { "nPrograma", nPrograma}, { "nCiclo", nCiclo}   };

            return View(model);
        }


   

        public async Task<JsonResult> ListaAutoclaveJ()
        {
            
            List<CiclosSabiDos> _sabiDos = await _context.CiclosSabiDos.ToListAsync();
          
            var queryj = from x in _sabiDos.Where(x => x.IdAutoclave == "0827J").OrderByDescending(x => x.Id).Take(1) select x;
            var queryk = from x in _sabiDos.Where(x => x.IdAutoclave == "0828K").OrderByDescending(x => x.Id).Take(1) select x;
            var queryl = from x in _sabiDos.Where(x => x.IdAutoclave == "1167L").OrderByDescending(x => x.Id).Take(1) select x;
            var pc = queryj.Union(queryk).Union(queryl);

           
            return Json(pc);


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
            // if (ciclosAutoclaves.Programa.Trim().Equals("9") || ciclosAutoclaves.Programa.Trim().Equals("10"))
            if (ciclosInt > 0)

            {
                _printerNueveDiez.printNueveDiez(id);
            }


            if (ciclosAutoclaves == null)
            {
                return NotFound();
            }
            TempData["Print"] = "El Archivo ha sido Impreso";
            string EventoJ = "Re-Impresión";
            _log.Write(_httpContextAccessor.HttpContext.Session.GetString("SessionFullName"), DateTime.Now, EventoJ + " " + _httpContextAccessor.HttpContext.Session.GetString("AutoclaveNumeroJ"), _httpContextAccessor.HttpContext.Session.GetString("SessionComentarioJ"));

            return RedirectToAction("Index", "AutoClaveJ");
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
            int ciclosInt = Convert.ToInt32(ciclosAutoclaves.Programa.Trim());
            if (ciclosInt > 0)
            //if (ciclosAutoclaves.Programa.Trim().Equals("9") || ciclosAutoclaves.Programa.Trim().Equals("10"))

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
            string EventoJ = "Re-Impresión";
            _log.Write(_httpContextAccessor.HttpContext.Session.GetString("SessionFullName"), DateTime.Now, EventoJ + " " + _httpContextAccessor.HttpContext.Session.GetString("AutoclaveNumeroJ"), _httpContextAccessor.HttpContext.Session.GetString("SessionComentarioJ"));
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
            //  string path = @"\\essaappserver01\HojaResumen\API\AutoClaveJ\" + ciclo;

            var query = _context.MaestroAutoclave.Where(t => t.Matricula == "0827J").FirstOrDefault();
            var path = query.RutaSalida.ToString() + ciclo;


            string[] texts = System.IO.File.ReadAllLines(path, new UnicodeEncoding());
            ViewBag.Data = texts;


            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, "text/html", ciclo);
            // return View(ciclosAutoclaves);

        }

        public async Task<IActionResult> CycleList(string ciclo, int? page)
        {
            var query = _context.MaestroAutoclave.Where(t => t.Matricula == "0827J").FirstOrDefault();
            var path = query.RutaSalida.ToString();

            DirectoryInfo dir = new DirectoryInfo(path);
            List<DownloadViewModel> Cyclelist = new List<DownloadViewModel>();
            foreach (FileInfo file in dir.GetFiles())
            {
                Cyclelist.Add(new DownloadViewModel { Dir = file.FullName, Ciclo = file.FullName.Split('\\')[5], Numero = Convert.ToInt32(file.FullName.Split('\\')[5].Split('J')[1].Split('.')[0]) });
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
                var path = _config["CiclosPath:Path"] + $"AutoClaveJ/{ciclo}";
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

            var ciclosAutoclaves = await _context.CiclosSabiDos
                .FirstOrDefaultAsync(m => m.Id == id);


            if (ciclosAutoclaves == null)
            {
                return NotFound();
            }

            // ViewBag.datos = ciclosAutoclaves.Id;
            HttpContext.Session.SetString("SessionDatosJ", ciclosAutoclaves.Id.ToString());
            HttpContext.Session.SetString("AutoclaveNumeroJ", ("AutoClaveJ" + " " + "N°Ciclo:" + ciclosAutoclaves.NumeroCiclo).ToString());



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
                                    HttpContext.Session.SetString("SessionPassJ", model.Contraseña);
                                    HttpContext.Session.SetString("SessionNameJ", model.Usuario);
                                    HttpContext.Session.SetString("SessionComentarioJ", model.Comentario);
                                    //HttpContext.Session.SetString("SessionDatosJ", model.Dato);
                                    HttpContext.Session.SetString("SessionTiempoJ", DateTime.Now.ToString("HH:mm:ss"));

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




        // GET: AutoClaveJJ/Details/5
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

        // GET: AutoClaveJJ/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AutoClaveJJ/Create
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

        // GET: AutoClaveJJ/Edit/5
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

        // POST: AutoClaveJJ/Edit/5
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

        // GET: AutoClaveJJ/Delete/5
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

        // POST: AutoClaveJJ/Delete/5
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
