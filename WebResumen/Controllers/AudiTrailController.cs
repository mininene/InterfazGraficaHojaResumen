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
    public class AudiTrailController : Controller
    {
        private readonly AppDbContext _context;

        public AudiTrailController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AudiTrail
        public async Task<IActionResult> Index()
        {
            var result = from q in _context.AudiTrail.OrderByDescending(q => q.Id) select q;
            return View(await result.ToListAsync());
        }

        public async Task<JsonResult> ListAudiTrail()
        {

            var result = from q in _context.AudiTrail.OrderByDescending(q => q.Id) select q;
            
               return Json(await result.ToListAsync());
        }

    }
}
