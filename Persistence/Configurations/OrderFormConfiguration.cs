using Domain.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class OrderFormConfiguration : IEntityTypeConfiguration<OrderForm>
    {
        public void Configure(EntityTypeBuilder<OrderForm> builder)
        {
            builder.HasKey(of => of.ProcurementId);

            builder.Property(of => of.SerialNumber).IsRequired();
            builder.Property(of => of.Status)
                   .IsRequired()
                   .HasConversion(
                       v => v.ToString(),
                       v => (FormStatus)Enum.Parse(typeof(FormStatus), v)
                   );

            builder.Property(of => of.CreateAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(of => of.UpdateAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
