using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebResumen.Models;
using WebResumen.Models.ViewModels;
using WebResumen.Services.LogRecord;

namespace WebResumen.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly  ILogRecord _log;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(AppDbContext context,ILogRecord log, IHttpContextAccessor httpContextAccessor)
        {
            _log = log;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }



        public IActionResult Index(LoginViewModel model)
        {
           
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
                            if (user.IsAccountLockedOut())
                            {
                                if (user.IsMemberOf(groupAdmins) || user.IsMemberOf(groupSupervisors) || user.IsMemberOf(groupUsers))
                                {
                                    ///////////////////////////////////////////////////////////////////////
                                    using (var searcher = new DirectorySearcher(new DirectoryEntry(path)))
                                    {
                                        string fullName = string.Empty;
                                        DirectoryEntry de = (user.GetUnderlyingObject() as DirectoryEntry);
                                        if (de != null)
                                        { fullName = de.Properties["displayName"][0].ToString(); }
                                        string EventoIb = "Sesión Bloqueada";
                                        string ComentarioIb = "Bloqueo de sesion";
                                        _log.Write(fullName, DateTime.Now, EventoIb, ComentarioIb);
                                    }
                                }
                                   
                                    TempData["Bloqueo"] = "Cuenta Bloqueada";
                               
                                //return View();
                                return RedirectToAction("Index", "Home");

                            }
                            if (user.IsMemberOf(groupAdmins) || user.IsMemberOf(groupSupervisors) || user.IsMemberOf(groupUsers))
                            {
                                ///////////////////////////////////////////////////////////////////////
                                using (var searcher = new DirectorySearcher(new DirectoryEntry(path)))
                                {
                                    string fullName = string.Empty;
                                    DirectoryEntry de = (user.GetUnderlyingObject() as DirectoryEntry);
                                    if (de != null)
                                    { fullName = de.Properties["displayName"][0].ToString(); }

                                    ////////////////////////////////////////////////////////////
                                HttpContext.Session.SetString("SessionUser", model.Usuario);
                                HttpContext.Session.SetString("SessionPass", model.Contraseña);
                                HttpContext.Session.SetString("SessionName", fullName);
                                HttpContext.Session.SetString("SessionTiempo", DateTime.Now.AddMinutes(10).ToString("HH:mm:ss"));
                                string EventoI = "Inicio de sesión";
                                string ComentarioI = "Ha iniciado sesión";
                                _log.Write(fullName, DateTime.Now, EventoI, ComentarioI);
                                }
                                return RedirectToAction("Index", "Inicio");
                            }
                            else
                            {
                                TempData["Grupo"] = "No pertenece a al grupo";
                               
                                //return View();
                                return RedirectToAction("Index", "Home");
                               
                            }
                        }

                       
                    }
                    catch
                    {
                        using (PrincipalContext ctxo = new PrincipalContext(ContextType.Domain, dominio))
                        {
                           
                                UserPrincipal user = UserPrincipal.FindByIdentity(ctxo, model.Usuario);
                            if (user != null)
                            {
                                ///////////////////////////////////////////////////////////////////////
                                using (var searcher = new DirectorySearcher(new DirectoryEntry(path)))
                                {
                                    string fullName = string.Empty;
                                    DirectoryEntry de = (user.GetUnderlyingObject() as DirectoryEntry);
                                    if (de != null)
                                    { fullName = de.Properties["displayName"][0].ToString(); }


                                    string EventoIx = "Inicio sesión Fallido";
                                    string ComentarioIx = "Fallo Inicio de sesión";
                                    TempData["Fail"] = "Login Fallido. Contraseña Incorrecta";
                                    _log.Write(fullName, DateTime.Now, EventoIx, ComentarioIx);
                                }
                            }
                            else
                            {
                                TempData["Fail"] = "Login Fallido. Usuario Incorrecto";
                            }
                        }
                        
                        return View();
                    }
                 }
            }

           // TempData["Fail"] = "Login Fallido. Usuario o Contraseña Incorrecta";
            return View();


        }


        public IActionResult Logout()
        {
           
            try
            {
               
                string EventoI = "Cierre de sesión";
                string ComentarioI = "Ha Cerrado sesión";
                string usuario = _httpContextAccessor.HttpContext.Session.GetString("SessionName");
                _log.Write(usuario, DateTime.Now, EventoI, ComentarioI);
            }
            catch { }
      
            HttpContext.Session.SetString("SessionPass", "");
            HttpContext.Session.SetString("SessionUser", "");
            HttpContext.Session.Clear();
            HttpContext.Session.Remove(".AspNetCore.Session");
           



            return RedirectToAction("Index", "Home");

        }


        public IActionResult Tiempo()
        {
           
            var query = _context.Parametros.OrderBy(x=>x.Tsesion).FirstOrDefault();
            int? data = query.Tsesion;
            var fname = _httpContextAccessor.HttpContext.Session.GetString("SessionName");
            var result = Json(new
            {
                data,fname
            });
            return Json(result);

        }





    }
}
