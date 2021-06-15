using System;

namespace WebResumen.Services.LogRecord
{
    public interface ILogRecord
    {
        void Write(string Usuario, DateTime fechaHora, string Evento, string comentario);
    }
}
