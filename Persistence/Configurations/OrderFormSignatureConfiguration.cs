using Domain.Entities;
using Domain.Entities.Forms;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class OrderFormSignatureConfiguration : IEntityTypeConfiguration<Signature>
    {
        public void Configure(EntityTypeBuilder<Signature> builder)
        {
            builder.ToTable("OrderFormSignatures");
            builder.HasKey(s => s.SignatureId);

            builder.Property(s => s.SignatureId).ValueGeneratedOnAdd();
            builder.Property(s => s.FormId).IsRequired();
            builder.Property(s => s.UserId).IsRequired();
            builder.Property(s => s.Role)
                   .IsRequired()
                   .HasConversion(
                       role => role.Value,
                       value => Roles.FromValue(value)
                   );
            builder.Property(s => s.Memo).HasMaxLength(500);
            builder.Property(s => s.IsApproved).IsRequired();
            builder.Property(s => s.ApprovedAt).HasColumnType("datetime");
            builder.Property(s => s.IsRejected).IsRequired();
            builder.Property(s => s.RejectedAt).HasColumnType("datetime");

            builder.HasOne<Form>()
                   .WithMany()
                   .HasForeignKey(s => s.FormId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
