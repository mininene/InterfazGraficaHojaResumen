using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebResumen.Models;
using WebResumen.Models.ViewModels;

namespace WebResumen.Controllers
{
    [Authorize(Policy = "ADMIN")]
    public class MaestroAutoclaveController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public MaestroAutoclaveController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: MaestroAutoclave
        public async Task<IActionResult> Index()
        {
            return View(await _context.MaestroAutoclave.ToListAsync());
        }

        public async Task<JsonResult> ListMaestro()
        {
            var result = await _context.MaestroAutoclave.ToListAsync();
            
            return Json(result);


        }

        // GET: MaestroAutoclave/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maestroAutoclave = await _context.MaestroAutoclave
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maestroAutoclave == null)
            {
                return NotFound();
            }

            return View(maestroAutoclave);
        }

        // GET: MaestroAutoclave/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MaestroAutoclave/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Matricula,Nombre,Version,Ip,Seccion,Estado,UltimoCiclo,RutaSalida,RutaSalidaPdf")] MaestroAutoclave maestroAutoclave)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maestroAutoclave);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(maestroAutoclave);
        }

        // GET: MaestroAutoclave/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maestroAutoclave = await _context.MaestroAutoclave.FindAsync(id);
            if (maestroAutoclave == null)
            {
                return NotFound();
            }
            return View(maestroAutoclave);
        }

        // POST: MaestroAutoclave/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Matricula,Nombre,Version,Ip,Seccion,Estado,UltimoCiclo,RutaSalida,RutaSalidaPdf")] MaestroAutoclave maestroAutoclave)
        {
            if (id != maestroAutoclave.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var maestroAutoclaveToUpdate = await _context.MaestroAutoclave
                .FirstOrDefaultAsync(c => c.Id == id);

                    if (await TryUpdateModelAsync<MaestroAutoclave>(maestroAutoclaveToUpdate,
                        "",
                        c => c.Id, c => c.Matricula, c => c.Nombre, c => c.Version, c => c.Ip, c => c.Seccion, c => c.Estado, c => c.UltimoCiclo
                        , c => c.RutaSalida, c => c.RutaSalidaPdf))
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
                    //_context.Update(maestroAutoclave);
                    //await _context.SaveChangesAsync();
                    //var entry = _context.MaestroAutoclave.Attach(maestroAutoclave);
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
                    if (!MaestroAutoclaveExists(maestroAutoclave.Id))
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
            return View(maestroAutoclave);
        }

        ///////////////////////////////////////////////////////////////
        [HttpGet]
        public async Task<IActionResult> Login(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maestroAutoclave = await _context.MaestroAutoclave
                .FirstOrDefaultAsync(m => m.Id == id);


            if (maestroAutoclave == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetString("SessionNombreMS", "");
            HttpContext.Session.SetString("SessionDatosM", maestroAutoclave.Id.ToString());
            HttpContext.Session.SetString("SessionMatriculaM", maestroAutoclave.Matricula.ToString());
            HttpContext.Session.SetString("SessionNombreM", maestroAutoclave.Nombre.ToString());
            HttpContext.Session.SetString("SessionVersionM", maestroAutoclave.Version.ToString());
            HttpContext.Session.SetString("SessionIpM", maestroAutoclave.Ip.ToString());
            HttpContext.Session.SetString("SessionSeccionM", maestroAutoclave.Seccion.ToString());
            HttpContext.Session.SetString("SessionEstadoM", maestroAutoclave.Estado.ToString());
            HttpContext.Session.SetString("SessionUltimoCicloM", maestroAutoclave.UltimoCiclo.ToString());
            HttpContext.Session.SetString("SessionRutaSalidaM", maestroAutoclave.RutaSalida.ToString());
            HttpContext.Session.SetString("SessionRutaSalidaPDFM", maestroAutoclave.RutaSalidaPdf.ToString());



            return View("Login");

        }

        [HttpPost]
        public IActionResult Login(DoubleLoginMaestroViewModel model, int? id)
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

                                    HttpContext.Session.SetString("SessionPassM", model.Contraseña);
                                    HttpContext.Session.SetString("SessionNameM", model.Usuario);
                                    HttpContext.Session.SetString("SessionComentario", model.Comentario);

                                    //HttpContext.Session.SetString("SessionDatosM", model.Id);
                                    //HttpContext.Session.SetString("SessionMatriculaM", model.Matricula);
                                    //HttpContext.Session.SetString("SessionNombreM", model.Nombre);
                                    //HttpContext.Session.SetString("SessionVersionM", model.Version);
                                    //HttpContext.Session.SetString("SessionIpM", model.Ip);
                                    //HttpContext.Session.SetString("SessionSeccionM", model.Seccion);
                                    //HttpContext.Session.SetString("SessionEstadoM", model.Estado.ToString());
                                    //HttpContext.Session.SetString("SessionUltimoCicloM", model.UltimoCiclo);
                                    //HttpContext.Session.SetString("SessionRutaSalidaM", model.RutaSalida);
                                    //HttpContext.Session.SetString("SessionRutaSalidaPDFM", model.RutaSalidaPDF);

                                    HttpContext.Session.SetString("SessionTiempoP", DateTime.Now.ToString("HH:mm:ss"));
                                    return View("Edit");

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

















        // GET: MaestroAutoclave/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maestroAutoclave = await _context.MaestroAutoclave
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maestroAutoclave == null)
            {
                return NotFound();
            }

            return View(maestroAutoclave);
        }

        // POST: MaestroAutoclave/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maestroAutoclave = await _context.MaestroAutoclave.FindAsync(id);
            _context.MaestroAutoclave.Remove(maestroAutoclave);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaestroAutoclaveExists(int id)
        {
            return _context.MaestroAutoclave.Any(e => e.Id == id);
        }
    }
}
