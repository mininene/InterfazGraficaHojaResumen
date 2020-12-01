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
    [Authorize(Policy = "ADTodos")]

   
    public class AutoClaveJController : Controller
    {
        private readonly AppDbContext _context;

        public AutoClaveJController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AutoClaveJJ
        public async Task<IActionResult> Index(string nCiclo, string nPrograma, string fecha)
        {
            List<ViewModelAutoClaveJ> _autoJ = new List<ViewModelAutoClaveJ>();
            List<CiclosSabiDos> _sabiDos = await _context.CiclosSabiDos.ToListAsync();


            var query = from x in _sabiDos.Where(x => x.IdAutoclave == "0827J").OrderByDescending(X=>X.Id).Take(50) select x;

            if (!String.IsNullOrEmpty(nCiclo))
            {
                query = query.Where(x => x.NumeroCiclo.Contains(nCiclo));

            }

            if (!String.IsNullOrEmpty(nPrograma))
            {
                query = query.Where(x => x.Programa.Contains(nPrograma));
            }



            if (!String.IsNullOrEmpty(fecha))
            {
                query = query.Where(x => x.HoraFin.Contains(fecha));

            }

            if (!String.IsNullOrEmpty(nCiclo) && !String.IsNullOrEmpty(nPrograma) && !String.IsNullOrEmpty(fecha))
            {
                query = query.Where(x => x.NumeroCiclo.Contains(nCiclo)
                                       || x.Programa.Contains(nPrograma)
                                         || x.HoraFin.Contains(fecha));  // si pongo la fecha como string si que lo coge
            }

            return View(query);
        }

        // GET: AutoClaveJJ/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciclosSabiDos = await _context.CiclosSabiDos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ciclosSabiDos == null)
            {
                return NotFound();
            }

            return View(ciclosSabiDos);
        }

        // GET: AutoClaveJJ/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AutoClaveJJ/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdAutoclave,IdSeccion,Tinicio,NumeroCiclo,Programa,Modelo,Programador,Operador,CodigoProducto,Lote,Notas,IdUsuario,Fase1,DuracionTotalF1,Fase2,DuracionTotalF2,Tif2,TisubF2,Tff2,TfsubF2,Fase3,DuracionTotalF3,Tif3,TisubF3,Tff3,TfsubF3,Fase4,DuracionTotalF4,Tif4,TisubF4,Fase5,DuracionTotalF5,Fase6,DuracionTotalF6,Fase7A,DuracionTotalF7a,Fase8A,DuracionTotalF8a,Fase7B,DuracionTotalF7b,Fase8B,DuracionTotalF8b,Fase9,DuracionTotalF9,Tif9,TisubF9,Tff9,Fase10,DuracionTotalF10,Fase11,DuracionTotalF11,Fase12,Tif12,TisubF12,HoraInicio,HoraFin,EsterilizacionN,Tminima,Tmaxima,DuracionTotal,FtzMin,FtzMax,DifMaxMin,AperturaPuerta,TiempoCiclo,ErrorCiclo,FechaRegistro")] CiclosSabiDos ciclosSabiDos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ciclosSabiDos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ciclosSabiDos);
        }

        // GET: AutoClaveJJ/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciclosSabiDos = await _context.CiclosSabiDos.FindAsync(id);
            if (ciclosSabiDos == null)
            {
                return NotFound();
            }
            return View(ciclosSabiDos);
        }

        // POST: AutoClaveJJ/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdAutoclave,IdSeccion,Tinicio,NumeroCiclo,Programa,Modelo,Programador,Operador,CodigoProducto,Lote,Notas,IdUsuario,Fase1,DuracionTotalF1,Fase2,DuracionTotalF2,Tif2,TisubF2,Tff2,TfsubF2,Fase3,DuracionTotalF3,Tif3,TisubF3,Tff3,TfsubF3,Fase4,DuracionTotalF4,Tif4,TisubF4,Fase5,DuracionTotalF5,Fase6,DuracionTotalF6,Fase7A,DuracionTotalF7a,Fase8A,DuracionTotalF8a,Fase7B,DuracionTotalF7b,Fase8B,DuracionTotalF8b,Fase9,DuracionTotalF9,Tif9,TisubF9,Tff9,Fase10,DuracionTotalF10,Fase11,DuracionTotalF11,Fase12,Tif12,TisubF12,HoraInicio,HoraFin,EsterilizacionN,Tminima,Tmaxima,DuracionTotal,FtzMin,FtzMax,DifMaxMin,AperturaPuerta,TiempoCiclo,ErrorCiclo,FechaRegistro")] CiclosSabiDos ciclosSabiDos)
        {
            if (id != ciclosSabiDos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ciclosSabiDos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CiclosSabiDosExists(ciclosSabiDos.Id))
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
            return View(ciclosSabiDos);
        }

        // GET: AutoClaveJJ/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciclosSabiDos = await _context.CiclosSabiDos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ciclosSabiDos == null)
            {
                return NotFound();
            }

            return View(ciclosSabiDos);
        }

        // POST: AutoClaveJJ/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ciclosSabiDos = await _context.CiclosSabiDos.FindAsync(id);
            _context.CiclosSabiDos.Remove(ciclosSabiDos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CiclosSabiDosExists(int id)
        {
            return _context.CiclosSabiDos.Any(e => e.Id == id);
        }
    }
}
