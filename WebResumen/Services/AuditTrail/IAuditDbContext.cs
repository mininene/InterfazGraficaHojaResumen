using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebResumen.Models;

namespace WebResumen.Services.AuditTrail
{
    public interface IAuditDbContext
    {
        DbSet<AudiTrail> Audit { get; set; }
        ChangeTracker ChangeTracker { get; }
    }
}
