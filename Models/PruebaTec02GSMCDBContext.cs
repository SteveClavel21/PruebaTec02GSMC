using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PruebaTec02GSMC.Models
{
    public partial class PruebaTec02GSMCDBContext : DbContext
    {
        public PruebaTec02GSMCDBContext()
        {
        }

        public PruebaTec02GSMCDBContext(DbContextOptions<PruebaTec02GSMCDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Directore> Directores { get; set; } = null!;
        public virtual DbSet<Pelicula> Peliculas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=turururu;Initial Catalog=PruebaTec02GSMCDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Directore>(entity =>
            {
                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<Pelicula>(entity =>
            {
                entity.HasKey(e => e.PeliculaId)
                    .HasName("PK__Pelicula__5AC6FCCC2725091F");

                entity.Property(e => e.Imagen).HasColumnType("image");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Peliculas)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Peliculas__Id__267ABA7A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
