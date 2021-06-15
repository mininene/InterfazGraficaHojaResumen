using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebResumen.Models;

namespace WebResumen.Controllers
{
    public class AudiTrailController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAuthorizationService _authorizationService;

        public AudiTrailController(AppDbContext context, IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        // GET: AudiTrail
        public async Task<IActionResult> Index()
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, "AdminSupervisor");
            if (authorizationResult.Succeeded)
            {
                var result = from q in _context.AudiTrail.OrderByDescending(q => q.Id) select q;
                return View(await result.ToListAsync());
            }
            else
            {
                return Redirect("/Inicio");
            }
        }

        public async Task<JsonResult> ListAudiTrail()
        {

            var result = from q in _context.AudiTrail.OrderByDescending(q => q.Id) select q;

            return Json(await result.ToListAsync());
        }

    }
}
