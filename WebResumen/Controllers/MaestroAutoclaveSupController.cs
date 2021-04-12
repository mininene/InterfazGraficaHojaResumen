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
    //[Authorize(Policy = "AdminSupervisor")]
    public class MaestroAutoclaveSupController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public MaestroAutoclaveSupController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: MaestroAutoclavesSup
        public async Task<IActionResult> Index()
        {
            return View(await _context.MaestroAutoclave.ToListAsync());
        }

        // GET: MaestroAutoclavesSup/Details/5
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

        // GET: MaestroAutoclavesSup/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MaestroAutoclavesSup/Create
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

        // GET: MaestroAutoclavesSup/Edit/5
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

        // POST: MaestroAutoclavesSup/Edit/5
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
                    //await _context.SaveChangesAsync();//Guardo el cambio realizado por mi persona N s eve valor antiguo y actual


                    //// Attach the object to the graph
                    //var entry = _context.MaestroAutoclave.Attach(maestroAutoclave);
                    //// Backup updated values
                    //var updated = entry.CurrentValues.Clone();
                    //// Reload entity from database, to track the original values
                    //entry.Reload();
                    //// Set the current values updated
                    //entry.CurrentValues.SetValues(updated);
                    //// Mark the entity as modified
                    //entry.State = EntityState.Modified;

                    //await _context.SaveChangesAsync();  // se ve valor antiguo y actual



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

            HttpContext.Session.SetString("SessionNombreM", "");
            HttpContext.Session.SetString("SessionDatosMS", maestroAutoclave.Id.ToString());
            HttpContext.Session.SetString("SessionMatriculaMS", maestroAutoclave.Matricula.ToString());
            HttpContext.Session.SetString("SessionNombreMS", maestroAutoclave.Nombre.ToString());
            HttpContext.Session.SetString("SessionVersionMS", maestroAutoclave.Version.ToString());
            HttpContext.Session.SetString("SessionIpMS", maestroAutoclave.Ip.ToString());
            HttpContext.Session.SetString("SessionSeccionMS", maestroAutoclave.Seccion.ToString());
            HttpContext.Session.SetString("SessionEstadoMS", maestroAutoclave.Estado.ToString());
            HttpContext.Session.SetString("SessionUltimoCicloMS", maestroAutoclave.UltimoCiclo.ToString());
            HttpContext.Session.SetString("SessionRutaSalidaMS", maestroAutoclave.RutaSalida.ToString());
            HttpContext.Session.SetString("SessionRutaSalidaPDFMS", maestroAutoclave.RutaSalidaPdf.ToString());



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

                                    HttpContext.Session.SetString("SessionPassMS", model.Contraseña);
                                    HttpContext.Session.SetString("SessionNameMS", model.Usuario);
                                    HttpContext.Session.SetString("SessionComentario", model.Comentario);

                                    //HttpContext.Session.SetString("SessionDatosMS", model.Id);
                                    //HttpContext.Session.SetString("SessionMatriculaMS", model.Matricula);
                                    //HttpContext.Session.SetString("SessionNombreMS", model.Nombre);
                                    //HttpContext.Session.SetString("SessionVersionMS", model.Version);
                                    //HttpContext.Session.SetString("SessionIpMS", model.Ip);
                                    //HttpContext.Session.SetString("SessionSeccionMS", model.Seccion);
                                    //HttpContext.Session.SetString("SessionEstadoMS", model.Estado.ToString());
                                    //HttpContext.Session.SetString("SessionUltimoCicloMS", model.UltimoCiclo);
                                    //HttpContext.Session.SetString("SessionRutaSalidaMS", model.RutaSalida);
                                    //HttpContext.Session.SetString("SessionRutaSalidaPDFMS", model.RutaSalidaPDF);
                       
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












        // GET: MaestroAutoclavesSup/Delete/5
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

        // POST: MaestroAutoclavesSup/Delete/5
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
