using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebResumen.Models.ViewModels
{
    public class JoinViewModel
    {
        public MaestroAutoclave MaestroList { get; set; }
        public CiclosAutoclaves SabiUnoList { get; set; }
        public CiclosSabiDos SabiDosList { get; set; }
    }
}
