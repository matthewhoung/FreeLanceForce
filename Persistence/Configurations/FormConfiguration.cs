using Domain.Entities.Forms;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class FormConfiguration : IEntityTypeConfiguration<Form>
    {
        public void Configure(EntityTypeBuilder<Form> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Id)
                .ValueGeneratedOnAdd();

            builder.Property(f => f.ProductId)
                .IsRequired();

            builder.Property(f => f.Stage)
                .IsRequired()
                .HasConversion(
                    stage => stage.Value,
                    value => Stages.FromValue(value)
                );

            builder.Property(f => f.CreateAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(f => f.UpdateAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            builder.HasMany<OrderForm>()
                  .WithOne()
                  .HasForeignKey(of => of.FormId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<AcceptanceForm>()
                   .WithOne()
                   .HasForeignKey(af => af.FormId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<PaymentForm>()
                   .WithOne()
                   .HasForeignKey(pf => pf.FormId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}