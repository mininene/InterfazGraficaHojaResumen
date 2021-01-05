using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebResumen.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AudiTrail> AudiTrail { get; set; }
        public virtual DbSet<CiclosAutoclaves> CiclosAutoclaves { get; set; }
        public virtual DbSet<CiclosSabiDos> CiclosSabiDos { get; set; }
        public virtual DbSet<MaestroAutoclave> MaestroAutoclave { get; set; }
        public virtual DbSet<Parametros> Parametros { get; set; }



        //AudiTrail


        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.Entries().Where(p => p.State == EntityState.Modified).ToList().ForEach(entry =>
            {
                Audit(entry);
            });
            return await base.SaveChangesAsync(cancellationToken);
        }



        private void Audit(EntityEntry entry)
        {
            IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();

            foreach (var property in entry.Properties)
            {
                //if (!property.IsModified)
                //    continue;
                if (property.IsModified)
                {
                    //var tiempoF = TimeSpan.Parse(DateTime.Now);
                    //var tiempoI = TimeSpan.Parse(_httpContextAccessor.HttpContext.Session.GetString("SessionTiempo"));
                   // var tiempoFx =  DateTime.Now.Subtract(Convert.ToDateTime(_httpContextAccessor.HttpContext.Session.GetString("SessionTiempo")));
                    //var difT = tiempoF - tiempoI;
                    //var dur = difT.TotalMinutes.ToString();
                    

                    var auditEntry = new AudiTrail
                    {
                        Usuario = _httpContextAccessor.HttpContext.Session.GetString("SessionName"),
                        Tabla = entry.Entity.GetType().Name,
                        Evento = Evento.Update.ToString()+" "+ _httpContextAccessor.HttpContext.Session.GetString("SessionNombreMS")+" " + _httpContextAccessor.HttpContext.Session.GetString("SessionNombreM"),
                        Campo = property.Metadata.Name,
                        Valor = property.OriginalValue.ToString(),
                        ValorActualizado = property.CurrentValue.ToString(),
                        FechaHora = DateTime.Now,
                        Comentario = _httpContextAccessor.HttpContext.Session.GetString("SessionComentario"),
                        Tiempo = Convert.ToDateTime(_httpContextAccessor.HttpContext.Session.GetString("SessionTiempo"))



                    };

                    this.AudiTrail.Add(auditEntry);
                }

            }
        }

        //AudiTrail





        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=ESSAAPPSERVER01;Initial Catalog=CiclosAutoclaves;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AudiTrail>(entity =>
            {
                entity.ToTable("AudiTrail");

                entity.Property(e => e.Campo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Comentario)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Evento)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaHora).HasColumnType("datetime");

                entity.Property(e => e.Tabla)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tiempo).HasColumnType("datetime");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Valor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ValorActualizado)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CiclosAutoclaves>(entity =>
            {
                entity.Property(e => e.AperturaPuerta)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DifMaxMin)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotal)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF10)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF11)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF12)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF3)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF4)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF5)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF6)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF7)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF8)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF9)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorCiclo).IsUnicode(false);

                entity.Property(e => e.EsterilizacionN)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase10)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase11)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase12)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase13)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase3)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase4)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase5)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase6)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase7)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase8)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase9)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.FtzMax)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FtzMin)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.HoraFin)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.HoraInicio)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdAutoclave)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdSeccion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdUsuario)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Lote)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Modelo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Notas)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroCiclo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Operador)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Programa)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Programador)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tff13)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TFF13");

                entity.Property(e => e.Tff5)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TFF5");

                entity.Property(e => e.Tff6)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TFF6");

                entity.Property(e => e.Tff8)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TFF8");

                entity.Property(e => e.Tff9)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TFF9");

                entity.Property(e => e.TfsubF13)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TFSubF13");

                entity.Property(e => e.TfsubF5)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TFSubF5");

                entity.Property(e => e.TfsubF6)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TFSubF6");

                entity.Property(e => e.TiempoCiclo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tif5)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TIF5");

                entity.Property(e => e.Tif6)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TIF6");

                entity.Property(e => e.Tif7)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TIF7");

                entity.Property(e => e.Tif8)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TIF8");

                entity.Property(e => e.Tif9)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TIF9");

                entity.Property(e => e.Tinicio)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TInicio");

                entity.Property(e => e.TisubF5)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TISubF5");

                entity.Property(e => e.TisubF6)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TISubF6");

                entity.Property(e => e.TisubF7)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TISubF7");

                entity.Property(e => e.TisubF8)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TISubF8");

                entity.Property(e => e.TisubF9)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TISubF9");

                entity.Property(e => e.Tmaxima)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TMaxima");

                entity.Property(e => e.Tminima)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TMinima");
            });

            modelBuilder.Entity<CiclosSabiDos>(entity =>
            {
                entity.Property(e => e.AperturaPuerta)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DifMaxMin)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotal)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF10)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF11)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF3)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF4)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF5)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF6)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF7a)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DuracionTotalF7A");

                entity.Property(e => e.DuracionTotalF7b)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DuracionTotalF7B");

                entity.Property(e => e.DuracionTotalF8a)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DuracionTotalF8A");

                entity.Property(e => e.DuracionTotalF8b)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DuracionTotalF8B");

                entity.Property(e => e.DuracionTotalF9)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorCiclo).IsUnicode(false);

                entity.Property(e => e.EsterilizacionN)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase10)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase11)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase12)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase3)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase4)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase5)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase6)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase7A)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase7B)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase8A)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase8B)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fase9)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.FtzMax)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FtzMin)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.HoraFin)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.HoraInicio)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdAutoclave)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdSeccion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdUsuario)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Lote)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Modelo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Notas)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroCiclo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Operador)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Programa)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Programador)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tff2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TFF2");

                entity.Property(e => e.Tff3)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TFF3");

                entity.Property(e => e.Tff9)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TFF9");

                entity.Property(e => e.TfsubF2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TFSubF2");

                entity.Property(e => e.TfsubF3)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TFSubF3");

                entity.Property(e => e.TiempoCiclo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tif12)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TIF12");

                entity.Property(e => e.Tif2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TIF2");

                entity.Property(e => e.Tif3)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TIF3");

                entity.Property(e => e.Tif4)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TIF4");

                entity.Property(e => e.Tif9)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TIF9");

                entity.Property(e => e.Tinicio)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TInicio");

                entity.Property(e => e.TisubF12)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TISubF12");

                entity.Property(e => e.TisubF2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TISubF2");

                entity.Property(e => e.TisubF3)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TISubF3");

                entity.Property(e => e.TisubF4)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TISubF4");

                entity.Property(e => e.TisubF9)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TISubF9");

                entity.Property(e => e.Tmaxima)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TMaxima");

                entity.Property(e => e.Tminima)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TMinima");
            });

            modelBuilder.Entity<MaestroAutoclave>(entity =>
            {
                entity.ToTable("MaestroAutoclave");

                entity.Property(e => e.Ip)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IP");

                entity.Property(e => e.Matricula)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RutaSalida)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.RutaSalidaPdf)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("RutaSalidaPDF");

                entity.Property(e => e.Seccion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UltimoCiclo)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Parametros>(entity =>
            {
                entity.Property(e => e.ImpresoraSabiDos)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ImpresoraSabiUno)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RutaLog)
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
