using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebResumen.Models.ViewModels;

namespace WebResumen.Controllers
{
    public class HomeController : Controller
    {
       
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                string dominio = @"global.baxter.com";
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
                                HttpContext.Session.SetString("SessionPass", model.Contraseña);
                                HttpContext.Session.SetString("SessionName", model.Usuario);
                                return RedirectToAction("Index", "Inicio");
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                               
                            }
                        }

                       
                    }
                    catch
                    { return RedirectToAction("Index", "Home"); }
                 }
            }

            ViewBag.fail = "Autenticación Fallida";
            return View();


        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("SessionPass", "");
            HttpContext.Session.SetString("SessionName", "");
           
            return RedirectToAction("Index", "Home");

        }




    }
}
