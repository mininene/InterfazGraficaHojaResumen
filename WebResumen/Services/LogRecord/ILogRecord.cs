using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebResumen.Services.LogRecord
{
    public interface ILogRecord
    {
        void Write(string Usuario, DateTime fechaHora, string Evento, string comentario );
    }
}
