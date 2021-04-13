using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebResumen.Services.LogError
{
    public interface ILogErrors
    {
        void write(string mensaje);
    }
}
