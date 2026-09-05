using AkademikWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AkademikWebAPI.Data
{
    public class AkademikDbContext : DbContext
    {
        public AkademikDbContext(DbContextOptions<AkademikDbContext> options) : base(options) { }
        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<Ders> Dersler { get; set; }
        public DbSet<OgrenciDers> OgrenciDersler { get; set; }
        public DbSet<Akademisyen> Akademisyenler { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=akademik.db");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OgrenciDers>()
                .HasKey(od => new { od.OgrenciNo, od.DersKodu });
        }
    }
}
