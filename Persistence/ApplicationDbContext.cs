using Domain.Forms;
using Microsoft.EntityFrameworkCore;
/*
 * 預先從NuGet安裝Microsoft.EntityFrameworkCore
 *               Microsoft.EntityFrameworkCore.Design
 *               Microsoft.EntityFrameworkCore.Tools
 * 之後打開NuGet Package Manager Console
 * 輸入: Add-Migration InitialCreate
 *      Update-Database -Project Persistence -StartupProject Persistence
 */

// old version
/*
namespace Persistence
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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
*/
namespace Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Form> Forms { get; set; }
        public DbSet<OrderForm> OrderForms { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}