using Domain.Forms;
using Microsoft.EntityFrameworkCore;

namespace FreeLanceForce.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Form> Forms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;User Id=postgres;Password=20240401;Database=VPlus_dev;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
