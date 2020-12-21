using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=ESSAAPPSERVER01;Initial Catalog=CiclosAutoclaves;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AudiTrail>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Comentario)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Evento)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaHora).HasColumnType("datetime");

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
                    .HasColumnName("TFF13")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tff5)
                    .HasColumnName("TFF5")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tff6)
                    .HasColumnName("TFF6")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tff8)
                    .HasColumnName("TFF8")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tff9)
                    .HasColumnName("TFF9")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TfsubF13)
                    .HasColumnName("TFSubF13")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TfsubF5)
                    .HasColumnName("TFSubF5")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TfsubF6)
                    .HasColumnName("TFSubF6")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TiempoCiclo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tif5)
                    .HasColumnName("TIF5")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tif6)
                    .HasColumnName("TIF6")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tif7)
                    .HasColumnName("TIF7")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tif8)
                    .HasColumnName("TIF8")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tif9)
                    .HasColumnName("TIF9")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tinicio)
                    .HasColumnName("TInicio")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TisubF5)
                    .HasColumnName("TISubF5")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TisubF6)
                    .HasColumnName("TISubF6")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TisubF7)
                    .HasColumnName("TISubF7")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TisubF8)
                    .HasColumnName("TISubF8")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TisubF9)
                    .HasColumnName("TISubF9")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tmaxima)
                    .HasColumnName("TMaxima")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tminima)
                    .HasColumnName("TMinima")
                    .HasMaxLength(100)
                    .IsUnicode(false);
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
                    .HasColumnName("DuracionTotalF7A")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF7b)
                    .HasColumnName("DuracionTotalF7B")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF8a)
                    .HasColumnName("DuracionTotalF8A")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DuracionTotalF8b)
                    .HasColumnName("DuracionTotalF8B")
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
                    .HasColumnName("TFF2")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tff3)
                    .HasColumnName("TFF3")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tff9)
                    .HasColumnName("TFF9")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TfsubF2)
                    .HasColumnName("TFSubF2")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TfsubF3)
                    .HasColumnName("TFSubF3")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TiempoCiclo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tif12)
                    .HasColumnName("TIF12")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tif2)
                    .HasColumnName("TIF2")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tif3)
                    .HasColumnName("TIF3")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tif4)
                    .HasColumnName("TIF4")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tif9)
                    .HasColumnName("TIF9")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tinicio)
                    .HasColumnName("TInicio")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TisubF12)
                    .HasColumnName("TISubF12")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TisubF2)
                    .HasColumnName("TISubF2")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TisubF3)
                    .HasColumnName("TISubF3")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TisubF4)
                    .HasColumnName("TISubF4")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TisubF9)
                    .HasColumnName("TISubF9")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tmaxima)
                    .HasColumnName("TMaxima")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Tminima)
                    .HasColumnName("TMinima")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MaestroAutoclave>(entity =>
            {
                entity.Property(e => e.Ip)
                    .HasColumnName("IP")
                    .HasMaxLength(50)
                    .IsUnicode(false);

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
                    .HasColumnName("RutaSalidaPDF")
                    .HasMaxLength(300)
                    .IsUnicode(false);

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
