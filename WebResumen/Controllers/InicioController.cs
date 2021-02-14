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
using WebResumen.Models.ViewModels;
using WebResumen.Services.LogRecord;

namespace WebResumen.Controllers
{
   

    [Authorize(Policy = "ADTodos")]
    public class InicioController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogRecord _log;
        IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();



        public InicioController(AppDbContext context, ILogRecord log)
        {
            _context = context;
            _log = log;
         }

        // GET: Inicio
        public async Task<IActionResult> Index()
        {
            return View(await _context.MaestroAutoclave.ToListAsync());
        }

        public async Task<JsonResult> ListHome()
        {
            
            var result = await _context.MaestroAutoclave.OrderBy(x=>x.Id).ToListAsync();
            //var multipleOrder = from t1 in result
            //                    from t2 in sabiUno
            //                    where (Convert.ToInt32(t1.UltimoCiclo) - 1).ToString() == t2.NumeroCiclo
            //                    where (t1.Matricula == t2.IdAutoclave)
            //                    select new
            //                    {
            //                        t1.Nombre,
            //                        t1.Estado,
            //                        t1.Matricula,
            //                        t2.NumeroCiclo,
            //                        t2.HoraFin,                                                                 

            //                    };

            //var multipleOrder2 = from t1 in result
            //                    from t3 in sabiDos
            //                    where (Convert.ToInt32(t1.UltimoCiclo) - 1).ToString() == t3.NumeroCiclo
            //                    where (t1.Matricula == t3.IdAutoclave)
            //                    select new
            //                    {
            //                        t1.Nombre,
            //                        t1.Estado,
            //                        t1.Matricula,
            //                        t3.NumeroCiclo,
            //                        t3.HoraFin,


            //                    };

            //var pc = multipleOrder.Union(multipleOrder2);
           
        

            return Json(result, ViewBag.Message);


        }


        public async Task<JsonResult> ListHomex()
        {

            //var result = await _context.MaestroAutoclave.OrderBy(x => x.Id).ToListAsync();
            var result = await _context.MaestroAutoclave.OrderBy(x => x.Id).ToListAsync();
            var sabiUno = await _context.CiclosAutoclaves.OrderByDescending(x => x.Id).ToListAsync();
            var sabiDos = await _context.CiclosSabiDos.OrderByDescending(x => x.Id).ToListAsync();

            


            var multiple = from t1 in result
                           join t2 in sabiUno on (Convert.ToInt32(t1.UltimoCiclo) - 1).ToString().Trim() equals t2.NumeroCiclo.Trim() into table1
                           from t2 in table1.DefaultIfEmpty()
                           join t3 in sabiDos on (Convert.ToInt32(t1.UltimoCiclo) - 1).ToString().Trim() equals t3.NumeroCiclo.Trim() into table2
                           from t3 in table2.DefaultIfEmpty()
                           select new JoinViewModel

                           {
                               MaestroList = t1,
                               SabiUnoList = t2,
                               SabiDosList = t3,

                           };
            var multipleOrder = multiple.OrderBy(x => x.MaestroList.Id);



            return Json( multipleOrder, ViewBag.Message);

           


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
