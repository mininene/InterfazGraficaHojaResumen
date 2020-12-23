using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebResumen.Models;

namespace WebResumen.Controllers
{
    [Authorize(Policy = "ADAS")]
    public class MaestroAutoclaveController : Controller
    {
        private readonly AppDbContext _context;

        public MaestroAutoclaveController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MaestroAutoclave
        public async Task<IActionResult> Index()
        {
            return View(await _context.MaestroAutoclave.ToListAsync());
        }

        public async Task<JsonResult> ListMaestro()
        {
            var result = await _context.MaestroAutoclave.ToListAsync();
            
            return Json(result);


        }

        // GET: MaestroAutoclave/Details/5
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

        // GET: MaestroAutoclave/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MaestroAutoclave/Create
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

        // GET: MaestroAutoclave/Edit/5
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

        // POST: MaestroAutoclave/Edit/5
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
                    //_context.Update(maestroAutoclave);
                    //await _context.SaveChangesAsync();
                    var entry = _context.MaestroAutoclave.Attach(maestroAutoclave);
                    // Backup updated values
                    var updated = entry.CurrentValues.Clone();
                    // Reload entity from database, to track the original values
                    entry.Reload();
                    // Set the current values updated
                    entry.CurrentValues.SetValues(updated);
                    // Mark the entity as modified
                    entry.State = EntityState.Modified;

                    _context.SaveChanges();
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

        // GET: MaestroAutoclave/Delete/5
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

        // POST: MaestroAutoclave/Delete/5
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
