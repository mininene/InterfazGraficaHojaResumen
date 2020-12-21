using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebResumen.Models;


namespace WebResumen.Controllers
{
   

    [Authorize(Policy = "ADTodos")]
    public class InicioController : Controller
    {
        private readonly AppDbContext _context;

      
        public InicioController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Inicio
        public async Task<IActionResult> Index()
        {
            foreach(var n in _context.MaestroAutoclave)
            {
                var actual = Regex.Replace(n.UltimoCiclo, "\\d+",
                m => (int.Parse(m.Value) - 1).ToString(new string('0', m.Value.Length)));
              
                List<UltimoCiclo> ultimo = new List<UltimoCiclo>();
                UltimoCiclo ult = new UltimoCiclo
                {
                   
                    ciclo = actual,
                  
                }; ultimo.Add(ult);
                ViewBag.data = ultimo.ToList();
            }
           



            return View(await _context.MaestroAutoclave.ToListAsync());
        }

        public async Task<JsonResult> ListHome()
        {
            var result = await _context.MaestroAutoclave.ToListAsync();
            //return View(await _context.CiclosAutoclaves.OrderByDescending(x=>x.Id).ToListAsync());
                      
           
            return Json(result, ViewBag.Message);


        }





        // GET: Inicio/Details/5
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

        // GET: Inicio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inicio/Create
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

        // GET: Inicio/Edit/5
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

        // POST: Inicio/Edit/5
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
                    _context.Update(maestroAutoclave);
                    await _context.SaveChangesAsync();
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

        // GET: Inicio/Delete/5
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

        // POST: Inicio/Delete/5
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
