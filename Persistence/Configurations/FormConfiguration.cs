using Domain.Forms;
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
            builder.Property(f => f.Title)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(f => f.Description)
                .HasMaxLength(500);
            builder.Property(f => f.Stage)
                .HasConversion
                (
                 stage => stage.ToString(),
                 stage => (FormStage)Enum.Parse(typeof(FormStage), stage)
                );
            builder.Property(f => f.CreateAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(f => f.UpdateAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasMany(f => f.OrderForms)
                   .WithOne(of => of.Form)
                   .HasForeignKey(of => of.FormId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
