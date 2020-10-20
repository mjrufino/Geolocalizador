using System;
using API_GEO.Data.DB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API_GEO.Data.DB
{
    public partial class geodbContext : DbContext
    {
        public geodbContext()
        {
        }

        public geodbContext(DbContextOptions<geodbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GeoLocalizacion> GeoLocalizacion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //TODO mover a startup y usar las configs desde appsettings.json
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=mysql;port=3306;user=root;password=admin1234;database=geodb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GeoLocalizacion>(entity =>
            {
                entity.ToTable("geo_localizacion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Calle)
                    .IsRequired()
                    .HasColumnName("calle")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Ciudad)
                    .HasColumnName("ciudad")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.CodigoPostal)
                    .HasColumnName("codigo_postal")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Latitud)
                    .HasColumnName("latitud")
                    .HasColumnType("float(17,15)");

                entity.Property(e => e.Longitud)
                    .HasColumnName("longitud")
                    .HasColumnType("float(18,15)");

                entity.Property(e => e.Numero)
                    .HasColumnName("numero")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Pais)
                    .HasColumnName("pais")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.OSMId).HasColumnName("osm_id");

                entity.Property(e => e.Procesando)
                    .HasColumnName("procesando")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.Provincia)
                    .HasColumnName("provincia")
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
