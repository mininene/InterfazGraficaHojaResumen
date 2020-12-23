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
    public class MaestroAutoclaveSupController : Controller
    {
        private readonly AppDbContext _context;

        public MaestroAutoclaveSupController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MaestroAutoclavesSup
        public async Task<IActionResult> Index()
        {
            return View(await _context.MaestroAutoclave.ToListAsync());
        }

        // GET: MaestroAutoclavesSup/Details/5
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

        // GET: MaestroAutoclavesSup/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MaestroAutoclavesSup/Create
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

        // GET: MaestroAutoclavesSup/Edit/5
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

        // POST: MaestroAutoclavesSup/Edit/5
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
                    //await _context.SaveChangesAsync();//Guardo el cambio realizado por mi persona

                 
                    // Attach the object to the graph
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

        // GET: MaestroAutoclavesSup/Delete/5
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

        // POST: MaestroAutoclavesSup/Delete/5
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
