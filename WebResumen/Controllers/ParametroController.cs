using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;
using WebResumen.Models;
using WebResumen.Models.ViewModels;
using WebResumen.Services.LogRecord;

namespace WebResumen.Controllers
{

    //[Authorize(Policy = "ADMIN")]

    public class ParametroController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogRecord _log;
        private readonly IConfiguration _config;
        private readonly IAuthorizationService _authorizationService;


        public ParametroController(AppDbContext context, ILogRecord log, IConfiguration config, IAuthorizationService authorizationService)
        {
            _context = context;
            _log = log;
            _config = config;
            _authorizationService = authorizationService;
        }

        // GET: Parametro
        public async Task<IActionResult> Index()

        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, "Admins");
            if (authorizationResult.Succeeded)
            {
                return View(await _context.Parametros.ToListAsync());
            }
            else
            {

                return Redirect("/Inicio");
            }
        }

        // GET: Parametro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parametros = await _context.Parametros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parametros == null)
            {
                return NotFound();
            }

            return View(parametros);
        }

        // GET: Parametro/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parametro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImpresoraSabiUno,ImpresoraSabiDos,RutaLog,Tiempo,Tsesion,Reinicio")] Parametros parametros)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parametros);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parametros);
        }

        // GET: Parametro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parametros = await _context.Parametros.FindAsync(id);
            if (parametros == null)
            {
                return NotFound();
            }
            return View(parametros);
        }

        // POST: Parametro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImpresoraSabiUno,ImpresoraSabiDos,RutaLog,Tiempo,Tsesion,Reinicio")] Parametros parametros)
        {
            if (id != parametros.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var parametrosToUpdate = await _context.Parametros
                    .FirstOrDefaultAsync(c => c.Id == id);

                    if (await TryUpdateModelAsync<Parametros>(parametrosToUpdate,
                        "",
                        c => c.Id, c => c.ImpresoraSabiUno, c => c.ImpresoraSabiDos, c => c.RutaLog, c => c.Tiempo, c => c.Tsesion, c => c.Reinicio))
                    {
                        try
                        {
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateException /* ex */)
                        {
                            //Log the error (uncomment ex variable name and write a log.)
                            ModelState.AddModelError("", "Unable to save changes. " +
                                "Try again, and if the problem persists, " +
                                "see your system administrator.");
                        }
                    }

                    //_context.Update(parametros);
                    //await _context.SaveChangesAsync();
                    // Attach the object to the graph
                    //id = parametros.Id;

                    //var entry = _context.Parametros.Attach(parametros);
                    //// Backup updated values
                    //var updated = entry.CurrentValues.Clone();
                    //// Reload entity from database, to track the original values
                    //entry.Reload();
                    //// Set the current values updated
                    //entry.CurrentValues.SetValues(updated);
                    //// Mark the entity as modified
                    //entry.State = EntityState.Modified;

                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParametrosExists(parametros.Id))
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
            return View(parametros);
        }


        ///////////////////////////////////////////////////////////////
        [HttpGet]
        public async Task<IActionResult> Login(int? id)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, "Admins");
            if (authorizationResult.Succeeded)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var parametros = await _context.Parametros
                    .FirstOrDefaultAsync(m => m.Id == id);


                if (parametros == null)
                {
                    return NotFound();
                }
                HttpContext.Session.SetString("SessionNombreM", "");
                HttpContext.Session.SetString("SessionNombreMS", "");
                HttpContext.Session.SetString("SessionDatosP", parametros.Id.ToString());
                HttpContext.Session.SetString("SessionImpresoraSabiUno", parametros.ImpresoraSabiUno.ToString());
                HttpContext.Session.SetString("SessionImpresoraSabiDos", parametros.ImpresoraSabiDos.ToString());
                HttpContext.Session.SetString("SessionRutaLog", parametros.RutaLog.ToString());
                HttpContext.Session.SetInt32("Tiempo", parametros.Tiempo);
                HttpContext.Session.SetInt32("TiempoS", Convert.ToInt32(parametros.Tsesion));
                HttpContext.Session.SetString("Reinicio", parametros.Reinicio.ToString());

                @ViewBag.datos = parametros;




                return View("Login");
            }
            else { return Redirect("/Inicio"); }
        }

        [HttpPost]
        public IActionResult Login(DoubleLoginParametrosViewModel model, int? id)
        {
            //var parametros = _context.Parametros
            //   .FirstOrDefaultAsync(m => m.Id == id);
            var parametros = _context.Parametros.FindAsync(id);

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

                                    HttpContext.Session.SetString("SessionPassP", model.Contraseña);
                                    HttpContext.Session.SetString("SessionNameP", model.Usuario);
                                    HttpContext.Session.SetString("SessionComentario", model.Comentario);

                                    //HttpContext.Session.SetString("SessionDatosP", model.Id);
                                    //HttpContext.Session.SetString("SessionImpresoraSabiUno", model.ImpresoraSabiUno);
                                    //HttpContext.Session.SetString("SessionImpresoraSabiDos", model.ImpresoraSabiDos);
                                    //HttpContext.Session.SetString("SessionRutaLog", model.RutaLog);
                                    //HttpContext.Session.SetInt32("Tiempo", model.Tiempo);
                                    //HttpContext.Session.SetString("Reinicio", model.Reinicio.ToString());

                                    HttpContext.Session.SetString("SessionTiempoP", DateTime.Now.ToString("HH:mm:ss"));
                                    return View("Edit");
                                    //return View("Edit" ,model.Dato);
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

            ViewBag.fail = "Autenticación Fallida";
            return View();


        }


        /////////////////////////////////////////////////////

















        // GET: Parametro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parametros = await _context.Parametros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parametros == null)
            {
                return NotFound();
            }

            return View(parametros);
        }

        // POST: Parametro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parametros = await _context.Parametros.FindAsync(id);
            _context.Parametros.Remove(parametros);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParametrosExists(int id)
        {
            return _context.Parametros.Any(e => e.Id == id);
        }
    }
}
