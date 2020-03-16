using DddInPractice.Logic;
using Microsoft.EntityFrameworkCore;

namespace DddInPractice.Data
{
    public class DddInPracticeContext : DbContext
    {
        public DbSet<SnackMachine> SnackMachines { get; set; }

        public DddInPracticeContext(DbContextOptions<DddInPracticeContext> options)
        : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SnackMachine>()
                    .Property(s => s.Id)
                    .HasColumnName("Id")
                    .IsRequired();

            modelBuilder.Entity<SnackMachine>().OwnsOne(s => s.MoneyInside, m =>
            {
                m.Property(p => p.OneCentCount)
                    .HasColumnName("OneCentCount")
                    .HasDefaultValue(0);
                m.Property(p => p.TenCentCount)
                    .HasColumnName("TenCentCount")
                    .HasDefaultValue(0);
                m.Property(p => p.QuarterCount)
                    .HasColumnName("QuarterCount")
                    .HasDefaultValue(0);
                m.Property(p => p.OneDollarCount)
                    .HasColumnName("OneDollarCount")
                    .HasDefaultValue(0);
                m.Property(p => p.FiveDollarCount)
                    .HasColumnName("FiveDollarCount")
                    .HasDefaultValue(0);
                m.Property(p => p.TwentyDollarCount)
                    .HasColumnName("TwentyDollarCount")
                    .HasDefaultValue(0);
            });

            modelBuilder.Entity<SnackMachine>().Ignore(s => s.MoneyInTransaction);
        }
    }
}