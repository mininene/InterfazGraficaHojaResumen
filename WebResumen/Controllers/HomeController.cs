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
        //private readonly SignInManager<IdentityUser> _signInManager;

        //public CuentaController(SignInManager<IdentityUser> signInManager)
        //{
        //    this._signInManager = signInManager;
        //}

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
                using (PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, dominio))
                {
                    bool userValid = principalContext.ValidateCredentials(model.Usuario, model.Contraseña);
                    if (userValid == true)
                    {
                        using (UserPrincipal user = UserPrincipal.FindByIdentity(principalContext, model.Usuario))
                        {
                            List<string> resultado = new List<string>();
                            WindowsIdentity wi = new WindowsIdentity(model.Usuario);


                            foreach (IdentityReference group in wi.Groups)
                            {
                                try
                                {
                                    resultado.Add(group.Translate(typeof(NTAccount)).ToString());
                                }
                                catch (Exception ex) { }
                                resultado.Sort();
                                var test = false;
                                foreach (var t in resultado)
                                {
                                    if (t.Equals("GLOBAL\\ESSA-HojaResumen_Users") || t.Equals("GLOBAL\\ESSA-HojaResumen_Admins") || t.Equals("GLOBAL\\ESSA-HojaResumen_Supervisors"))
                                    {
                                        test = true;
                                       
                                        HttpContext.Session.SetString("SessionPass", model.Contraseña);
                                        HttpContext.Session.SetString("SessionName", model.Usuario);
                                       
                                        return RedirectToAction("Index", "Homet");



                                    }
                                    if (test != true)
                                    {
                                        ModelState.AddModelError(string.Empty, "Intento de Login Invalido");
                                        //return Json("Contraseña Invalida");
                                    }
                                }
                            }
                        }
                    }

                }
               
            }
           
            ViewBag.fail = "Autenticación Fallida";
            return View(model);


        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("SessionPass", "");
            HttpContext.Session.SetString("SessionName", "");
           
            return RedirectToAction("Index", "Home");

        }




    }
}
