using Domain.Entities.Forms;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class PaymentFormConfiguration : IEntityTypeConfiguration<PaymentForm>
    {
        public void Configure(EntityTypeBuilder<PaymentForm> builder)
        {
            builder.HasKey(of => of.ProcurementId);

            builder.Property(of => of.FormId)
                   .IsRequired();

            builder.Property(of => of.SerialNumber)
                   .IsRequired();

            builder.Property(of => of.Status)
                   .IsRequired()
                   .HasConversion(
                       status => status.Value,
                       value => Status.FromValue(value)
                   );

            builder.Property(f => f.Title)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(f => f.Description)
                   .HasMaxLength(500);

            builder.Property(of => of.CreateAt)
                   .IsRequired()
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(of => of.UpdateAt)
                   .IsRequired()
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}