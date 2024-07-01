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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
