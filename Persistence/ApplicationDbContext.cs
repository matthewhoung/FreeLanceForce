using Domain.Forms;
using Microsoft.EntityFrameworkCore;


namespace Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Form> Forms { get; set; }
        public DbSet<OrderForm> OrderForms { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;User Id=postgres;Password=20240401;Database=VPlus_dev;");
        }
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}