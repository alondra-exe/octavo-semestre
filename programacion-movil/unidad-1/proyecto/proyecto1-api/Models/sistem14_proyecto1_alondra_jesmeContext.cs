using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace proyecto1_api.Models
{
    public partial class sistem14_proyecto1_alondra_jesmeContext : DbContext
    {
        public sistem14_proyecto1_alondra_jesmeContext()
        {
        }

        public sistem14_proyecto1_alondra_jesmeContext(DbContextOptions<sistem14_proyecto1_alondra_jesmeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alumno> Alumno { get; set; }
        public virtual DbSet<Docente> Docente { get; set; }
        public virtual DbSet<Progreso> Progreso { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//                optionsBuilder.UseMySql("server=204.93.167.23;user=sistem14_adjeg;password=alondrajesme032021;database=sistem14_proyecto1_alondra_jesme", x => x.ServerVersion("5.6.46-mysql"));
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.ToTable("alumno");

                entity.HasIndex(e => e.IdDocente)
                    .HasName("fk_IdDocente");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Eliminado).HasColumnType("bit(1)");

                entity.Property(e => e.IdDocente).HasColumnType("int(11)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.IdDocenteNavigation)
                    .WithMany(p => p.Alumno)
                    .HasForeignKey(d => d.IdDocente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_IdDocente");
            });

            modelBuilder.Entity<Docente>(entity =>
            {
                entity.ToTable("docente");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Clave).HasColumnType("int(4)");

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasColumnType("tinytext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Progreso>(entity =>
            {
                entity.ToTable("progreso");

                entity.HasIndex(e => e.IdAlumno)
                    .HasName("fk_IdAlumno");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.IdAlumno).HasColumnType("int(11)");

                entity.Property(e => e.Intentos).HasColumnType("int(11)");

                entity.Property(e => e.Puntuacion).HasColumnType("int(11)");

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithMany(p => p.Progreso)
                    .HasForeignKey(d => d.IdAlumno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_IdAlumno");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
