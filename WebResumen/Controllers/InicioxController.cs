﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebResumen.Models;

namespace WebResumen.Controllers
{
   
    //[Authorize(Policy = "ADUsers")]
   
   // [Authorize(Policy = "ADSupervisors")]
    public class InicioxController : Controller
    {
        private readonly ILogger<InicioxController> _logger;

        public InicioxController(ILogger<InicioxController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
