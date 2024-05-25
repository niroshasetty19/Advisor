using AdvisorApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvisorApp.Infrastructure.Persistence
{
    public class AdvisorDbContext : DbContext
    {
        public AdvisorDbContext(DbContextOptions<AdvisorDbContext> options)
            : base(options)
        {
        }
        public DbSet<Advisor> Advisors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Advisor>()
                .Property(a => a.Name)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Advisor>()
                .Property(a => a.SIN)
                .HasMaxLength(9)
                .IsRequired();

            modelBuilder.Entity<Advisor>()
                .Property(a => a.Address)
                .HasMaxLength(255);

            modelBuilder.Entity<Advisor>()
                .Property(a => a.Phone)
                .HasMaxLength(8);

            modelBuilder.Entity<Advisor>()
                .HasIndex(a => a.SIN)
                .IsUnique();
        }
    }
}
