using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;
using WebResumen.Models;
using WebResumen.Services.LogError;

namespace WebResumen.Controllers
{

    //[Authorize(Policy = "ADUsers")]

    // [Authorize(Policy = "ADSupervisors")]
    public class HomexxController : Controller
    {
        private readonly ILogger<HomexxController> _logger;
        private readonly ILogErrors _log;

        public HomexxController(ILogger<HomexxController> logger, ILogErrors log)
        {
            _logger = logger;
            _log = log;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int code)
        {

            var ErrorStatusCode = code;
            // Retrieve error information in case of internal errors
            try
            {
                var path = HttpContext
                          .Features
                          .Get<IExceptionHandlerPathFeature>();

                // Use the information about the exception 
                if (path != null)
                {
                    var exception = path.Error;
                    var pathString = path.Path;
                    _log.write($" {exception}   {pathString}  {exception.HResult}");
                    if (exception is FileNotFoundException)
                    {
                        return View(new ErrorViewModel { ExceptionMessage = "Archivo No Encontrado", RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

                    }
                    else
                    {
                        return View(new ErrorViewModel { ExceptionMessage = "Error mientras se procesa la solicitud", RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

                    }
                }
            }
            catch { }

            _log.write($"Error: {ErrorStatusCode.ToString()} ");
            if (ErrorStatusCode == 404)
            {
                return View(new ErrorViewModel { ExceptionMessage = "Página no Encontrada", RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

            }
            else if (ErrorStatusCode == 500)
            {
                return View(new ErrorViewModel { ExceptionMessage = "Error de Servidor", RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

            }
            else
            {
                return View(new ErrorViewModel { ExceptionMessage = "Error mientras se procesa la solicitud", RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

            }



        }
    }
}
