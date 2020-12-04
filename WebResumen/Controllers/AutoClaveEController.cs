using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebResumen.Models;
using WebResumen.Services.PrinterService;

namespace WebResumen.Controllers
{
    [Authorize(Policy = "ADTodos")]
    public class AutoClaveEController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPrinterOchoVeinte _printerOchoVeinte;
        private readonly IPrinterDosTresCuatro _printerDosTresCuatro;

        public AutoClaveEController(AppDbContext context, IPrinterOchoVeinte printerOchoVeinte, IPrinterDosTresCuatro printerDosTresCuatro)
        {
            _context = context;
            _printerOchoVeinte = printerOchoVeinte;
            _printerDosTresCuatro = printerDosTresCuatro;
        }

        // GET: AutoClaveE
        public async Task<IActionResult> Index(string nCiclo, string nPrograma, string fecha)
        {
            List<CiclosAutoclaves> _sabiUno = await _context.CiclosAutoclaves.ToListAsync();


            var query = from x in _sabiUno.Where(x => x.IdAutoclave == "NF1029E").OrderByDescending(X => X.Id).Take(50) select x;
           
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


        public async Task<IActionResult> Print(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ciclosAutoclaves = await _context.CiclosAutoclaves
               .FirstOrDefaultAsync(m => m.Id == id);

            if (ciclosAutoclaves.Programa.Trim().Equals("8") || ciclosAutoclaves.Programa.Trim().Equals("20"))

            {
                _printerOchoVeinte.printOchoVeinte(id);
            }

            if (ciclosAutoclaves.Programa.Trim().Equals("2") || ciclosAutoclaves.Programa.Trim().Equals("3") || ciclosAutoclaves.Programa.Trim().Equals("4"))
            {
                _printerDosTresCuatro.printDosTresCuatro(id);
            }


            if (ciclosAutoclaves == null)
            {
                return NotFound();
            }


            return View(ciclosAutoclaves);
        }














            // GET: AutoClaveE/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciclosAutoclaves = await _context.CiclosAutoclaves
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ciclosAutoclaves == null)
            {
                return NotFound();
            }

            return View(ciclosAutoclaves);
        }

        // GET: AutoClaveE/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AutoClaveE/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdAutoclave,IdSeccion,Tinicio,NumeroCiclo,Programa,Modelo,Programador,Operador,CodigoProducto,Lote,Notas,IdUsuario,Fase1,DuracionTotalF1,Fase2,DuracionTotalF2,Fase3,DuracionTotalF3,Fase4,DuracionTotalF4,Fase5,DuracionTotalF5,Tif5,TisubF5,Tff5,TfsubF5,Fase6,DuracionTotalF6,Tif6,TisubF6,Tff6,TfsubF6,Fase7,DuracionTotalF7,Tif7,TisubF7,Fase8,DuracionTotalF8,Tif8,TisubF8,Tff8,Fase9,DuracionTotalF9,Tif9,TisubF9,Tff9,Fase10,DuracionTotalF10,Fase11,DuracionTotalF11,Fase12,DuracionTotalF12,Fase13,Tff13,TfsubF13,HoraInicio,HoraFin,EsterilizacionN,Tminima,Tmaxima,DuracionTotal,FtzMin,FtzMax,DifMaxMin,AperturaPuerta,TiempoCiclo,ErrorCiclo,FechaRegistro")] CiclosAutoclaves ciclosAutoclaves)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ciclosAutoclaves);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ciclosAutoclaves);
        }

        // GET: AutoClaveE/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciclosAutoclaves = await _context.CiclosAutoclaves.FindAsync(id);
            if (ciclosAutoclaves == null)
            {
                return NotFound();
            }
            return View(ciclosAutoclaves);
        }

        // POST: AutoClaveE/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdAutoclave,IdSeccion,Tinicio,NumeroCiclo,Programa,Modelo,Programador,Operador,CodigoProducto,Lote,Notas,IdUsuario,Fase1,DuracionTotalF1,Fase2,DuracionTotalF2,Fase3,DuracionTotalF3,Fase4,DuracionTotalF4,Fase5,DuracionTotalF5,Tif5,TisubF5,Tff5,TfsubF5,Fase6,DuracionTotalF6,Tif6,TisubF6,Tff6,TfsubF6,Fase7,DuracionTotalF7,Tif7,TisubF7,Fase8,DuracionTotalF8,Tif8,TisubF8,Tff8,Fase9,DuracionTotalF9,Tif9,TisubF9,Tff9,Fase10,DuracionTotalF10,Fase11,DuracionTotalF11,Fase12,DuracionTotalF12,Fase13,Tff13,TfsubF13,HoraInicio,HoraFin,EsterilizacionN,Tminima,Tmaxima,DuracionTotal,FtzMin,FtzMax,DifMaxMin,AperturaPuerta,TiempoCiclo,ErrorCiclo,FechaRegistro")] CiclosAutoclaves ciclosAutoclaves)
        {
            if (id != ciclosAutoclaves.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ciclosAutoclaves);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CiclosAutoclavesExists(ciclosAutoclaves.Id))
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
            return View(ciclosAutoclaves);
        }

        // GET: AutoClaveE/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciclosAutoclaves = await _context.CiclosAutoclaves
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ciclosAutoclaves == null)
            {
                return NotFound();
            }

            return View(ciclosAutoclaves);
        }

        // POST: AutoClaveE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ciclosAutoclaves = await _context.CiclosAutoclaves.FindAsync(id);
            _context.CiclosAutoclaves.Remove(ciclosAutoclaves);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CiclosAutoclavesExists(int id)
        {
            return _context.CiclosAutoclaves.Any(e => e.Id == id);
        }
    }
}
