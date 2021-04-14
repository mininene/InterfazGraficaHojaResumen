using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebResumen.Services.LogError
{
    public class LogErrors : ILogErrors
    {
        private readonly IConfiguration _config;
        public LogErrors(IConfiguration config)
        {
            _config = config;

        }
       
        public void write(string mensaje)
        {

            string _path = _config["OptionalSettings:LogWeb"] + "\\LogWeb";
           // string _path = @"C:\Program Files\HojaResumen\old\" + "Log";
            Directory.CreateDirectory(_path);

            var fecha = DateTime.Now.ToString("dd/MM/yyyy");
            var hora = DateTime.Now.ToString("HH:mm:ss");
            using (var sw = new StreamWriter(_path + "/WebResumen_" + fecha.ToString().Replace("/", "") + ".Log", true))
            {
                sw.WriteLine($"[{fecha}  {hora}]  { mensaje}");
            }
           
        }
    }
}
