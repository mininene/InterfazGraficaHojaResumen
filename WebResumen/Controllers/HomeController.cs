﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using WebResumen.Models;
using WebResumen.Models.ViewModels;
using WebResumen.Services.LogRecord;

namespace WebResumen.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogRecord _log;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;
        public HomeController(AppDbContext context, ILogRecord log, IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            _log = log;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _config = config;
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
                                TempData["Grupo"] = "El Usuario No pertenece al grupo";

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
                                    TempData["Fail"] = "Inicio de Sesión Fallido. Contraseña Incorrecta";
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

        public IActionResult LogoutAuto()
        {

            try
            {

                string Eventox = "Cierre de sesión automático";
                string Comentariox = "Sesión expirada";
                string usuario = _httpContextAccessor.HttpContext.Session.GetString("SessionName");
                _log.Write(usuario, DateTime.Now, Eventox, Comentariox);
            }
            catch { }

            HttpContext.Session.SetString("SessionPass", "");
            HttpContext.Session.SetString("SessionUser", "");
            HttpContext.Session.Clear();
            HttpContext.Session.Remove(".AspNetCore.Session");




            return RedirectToAction("Index", "Home");

        }

        public IActionResult LogoutEmpty()
        {


            HttpContext.Session.SetString("SessionPass", "");
            HttpContext.Session.SetString("SessionUser", "");
            HttpContext.Session.Clear();
            HttpContext.Session.Remove(".AspNetCore.Session");

            return RedirectToAction("Index", "Home");

        }


        public IActionResult Tiempo()
        {

            var query = _context.Parametros.OrderBy(x => x.Tsesion).FirstOrDefault();
            int? data = query.Tsesion;
            var fname = _httpContextAccessor.HttpContext.Session.GetString("SessionName");
            var name = _httpContextAccessor.HttpContext.Session.GetString("SessionUser");
            var result = Json(new
            {
                data,
                fname,
                name
            });
            return Json(result);

        }





    }
}
