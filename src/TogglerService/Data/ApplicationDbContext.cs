using Microsoft.EntityFrameworkCore;
using TogglerService.Models;

namespace TogglerService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<GlobalToggle> GlobalToggles { get; set; }

        public DbSet<ServiceToggle> ServiceToggles { get; set; }

        public DbSet<ExcludedService> ExcludedServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GlobalToggle>(b =>
            {
                b.HasKey(t => t.Id);
                b.Property(t => t.Id)
                .HasMaxLength(50);

                b.HasMany(t => t.ExcludedServices)
                .WithOne(e => e.GlobalToggle);
                


            });
            modelBuilder.Entity<ExcludedService>(b =>
            {
                b.HasKey(e => new { e.ToggleId, e.ServiceId });
                b.Property(t => t.ServiceId)
                .HasMaxLength(50);
                b.Property(t => t.ToggleId)
                .HasMaxLength(50);


            });
            modelBuilder.Entity<ServiceToggle>(b =>
            {
                b.HasKey(t => new { t.Id, t.ServiceId });
                b.Property(t => t.Id)
                .HasMaxLength(50);
                b.Property(t => t.ServiceId)
                .HasMaxLength(50);
                b.Property(t => t.VersionRange)
                .HasMaxLength(50);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
