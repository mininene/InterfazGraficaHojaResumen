using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebResumen.Models;

namespace WebResumen.Controllers
{
    public class CiclosSabiUnoController : Controller
    {
        private readonly AppDbContext _context;

        public CiclosSabiUnoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CiclosSabiUno
        public async Task<IActionResult> Index()
        {
            return View(await _context.CiclosAutoclaves.ToListAsync());
        }

       
    }
}
