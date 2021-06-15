using System;
using System.Collections.Generic;
using WebResumen.Models;

namespace WebResumen.Services.LogRecord
{
    public class LogRecord : ILogRecord
    {
        private readonly AppDbContext _context;
        public LogRecord(AppDbContext context)
        {
            _context = context;

        }

        public void Write(string Usuario, DateTime fechaHora, string Evento, string comentario)
        {

            var listaInicio = new List<AudiTrail>
            {
                new AudiTrail { Usuario = Usuario, Tabla = "", FechaHora = fechaHora, Evento = Evento, Campo = "", Valor = "", ValorActualizado = "", Comentario = comentario, Tiempo = null },

            }; listaInicio.ForEach(s => _context.AudiTrail.Add(s));
            _context.SaveChanges();
        }


    }

}

