using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebResumen.Models;

namespace WebResumen.Controllers
{
    public class AutoClaveJController : Controller
    {


        private readonly AppDbContext _context;

        public AutoClaveJController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            //List<ViewModelAutoClaveJ> _autoJ = new List<ViewModelAutoClaveJ>();
            //List<CiclosSabiDos> _sabiDos = await _context.CiclosSabiDos.ToListAsync();


            //var query = from x in _sabiDos.Where(x => x.IdAutoclave == "0827J") select x;

            //return View(query);
           
                return View(await _context.CiclosSabiDos.ToListAsync());
            
        }
    }
}
