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
            var result = from s in _context.AudiTrail.OrderByDescending(s => s.Id) select s;
            return View(await result.ToListAsync());
        }

        // GET: AudiTrail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audiTrail = await _context.AudiTrail
                .FirstOrDefaultAsync(m => m.Id == id);
            if (audiTrail == null)
            {
                return NotFound();
            }

            return View(audiTrail);
        }

        // GET: AudiTrail/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AudiTrail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Usuario,FechaHora,Evento,Valor,ValorActualizado,Comentario")] AudiTrail audiTrail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(audiTrail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(audiTrail);
        }

        // GET: AudiTrail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audiTrail = await _context.AudiTrail.FindAsync(id);
            if (audiTrail == null)
            {
                return NotFound();
            }
            return View(audiTrail);
        }

        // POST: AudiTrail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Usuario,FechaHora,Evento,Valor,ValorActualizado,Comentario")] AudiTrail audiTrail)
        {
            if (id != audiTrail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(audiTrail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AudiTrailExists(audiTrail.Id))
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
            return View(audiTrail);
        }

        // GET: AudiTrail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audiTrail = await _context.AudiTrail
                .FirstOrDefaultAsync(m => m.Id == id);
            if (audiTrail == null)
            {
                return NotFound();
            }

            return View(audiTrail);
        }

        // POST: AudiTrail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var audiTrail = await _context.AudiTrail.FindAsync(id);
            _context.AudiTrail.Remove(audiTrail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AudiTrailExists(int id)
        {
            return _context.AudiTrail.Any(e => e.Id == id);
        }
    }
}
