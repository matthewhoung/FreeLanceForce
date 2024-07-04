using Domain.Entities;
using Domain.Entities.Forms;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations;

namespace Persistence
{
    public class ApplicationDbContext : DbContext
    {
        
        public DbSet<Form> Forms { get; set; }
        public DbSet<OrderForm> OrderForms { get; set; }
        public DbSet<AcceptanceForm> AcceptanceForms { get; set; }
        public DbSet<PaymentForm> PaymentForms { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Signature> OrderFormSignatures { get; set; }
        public DbSet<Signature> AcceptanceFormSignatures { get; set; }
        public DbSet<Signature> PaymentFormSignatures { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            modelBuilder.ApplyConfiguration(new SignatureConfiguration("OrderFormSignatures"));
            modelBuilder.ApplyConfiguration(new SignatureConfiguration("AcceptanceFormSignatures"));
            modelBuilder.ApplyConfiguration(new SignatureConfiguration("PaymentFormSignatures"));
        }
    }
}