using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebResumen.Models;

namespace WebResumen.Services.AuditTrail
{
    public class AppDbContextAuditable : AppDbContext
    {
        public override int SaveChanges()
        {
            ChangeTracker.Entries().Where(p => p.State == EntityState.Modified).ToList().ForEach(entry =>
            {
                Audit(entry);
            });

            return base.SaveChanges();
        }

        private void Audit(EntityEntry entry)
        {
            foreach (var property in entry.Properties)
            {
                if (!property.IsModified)
                    continue;

                var auditEntry = new AudiTrail
                {
                    //Table = entry.Entity.GetType().Name,
                    //Column = property.Metadata.Name,
                    Valor = property.OriginalValue.ToString(),
                    ValorActualizado = property.CurrentValue.ToString(),
                    FechaHora = DateTime.Now
                };

                this.AudiTrail.Add(auditEntry);
            }
        }
    }
}
