using GradeManagement.Models.Domain;
using Microsoft.EntityFrameworkCore;


namespace GradeManagement.Data
{
    
        public class GradeDbContext : DbContext
        {
            public GradeDbContext(DbContextOptions<GradeDbContext> options) : base(options) { }

            public DbSet<Center> Centers { get; set; }
            public DbSet<Grade> Grades { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Center>()
                    .HasMany(c => c.Grades)
                    .WithOne(g => g.Center)
                    .HasForeignKey(g => g.CenterId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }
    
}
