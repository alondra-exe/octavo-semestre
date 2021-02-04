using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace pokemon_api.models
{
    public partial class sistem14_pokemonContext : DbContext
    {
        public sistem14_pokemonContext()
        {
        }

        public sistem14_pokemonContext(DbContextOptions<sistem14_pokemonContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pokemon> Pokemon { get; set; }
        public virtual DbSet<Tipos> Tipos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /* if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=204.93.167.23;database=sistem14_pokemon;user=sistem14_admin;password=admin2020", x => x.ServerVersion("5.6.46-mysql"));
            } */
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pokemon>(entity =>
            {
                entity.ToTable("pokemon");

                entity.HasIndex(e => e.Evolucion)
                    .HasName("fkev_idx");

                entity.HasIndex(e => e.Preevolucion)
                    .HasName("fkpre_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Abilidades)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Categoria)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Descripcion)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Evolucion).HasColumnType("int(11)");

                entity.Property(e => e.Imagen1)
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Imagen2)
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Nombre)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Preevolucion).HasColumnType("int(11)");

                entity.Property(e => e.Tipo1)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Tipo2)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.EvolucionNavigation)
                    .WithMany(p => p.InverseEvolucionNavigation)
                    .HasForeignKey(d => d.Evolucion)
                    .HasConstraintName("fkev");

                entity.HasOne(d => d.PreevolucionNavigation)
                    .WithMany(p => p.InversePreevolucionNavigation)
                    .HasForeignKey(d => d.Preevolucion)
                    .HasConstraintName("fkpre");
            });

            modelBuilder.Entity<Tipos>(entity =>
            {
                entity.ToTable("tipos");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Tipo)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Type)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
