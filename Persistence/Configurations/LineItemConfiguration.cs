using Domain.Entities;
using Domain.Entities.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class LineItemConfiguration : IEntityTypeConfiguration<LineItem>
    {
        public void Configure(EntityTypeBuilder<LineItem> builder)
        {
            builder.HasKey(li => li.LineItemId);

            builder.Property(li => li.LineItemId)
                .ValueGeneratedOnAdd();

            builder.Property(li => li.FormId)
                .IsRequired();

            builder.Property(li => li.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(li => li.Description)
                .HasMaxLength(1000);

            builder.Property(li => li.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(li => li.Quantity)
                .IsRequired();

            builder.Property(li => li.Total)
                 .HasColumnType("decimal(18,2)")
                 .IsRequired();

            builder.Property(li => li.IsApproved)
                .IsRequired();

            builder.Property(li => li.ApprovedAt)
                .HasColumnType("datetime");

            builder.Property(li => li.IsRejected)
                .IsRequired();

            builder.Property(li => li.RejectedAt)
                .HasColumnType("datetime");

            // Define the relationship with the Forms table
            builder.HasOne<Form>()
                .WithMany()
                .HasForeignKey(li => li.FormId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
