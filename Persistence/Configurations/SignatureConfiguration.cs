using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class SignatureConfiguration : IEntityTypeConfiguration<Signature>
    {
        private readonly string _tableName;

        public SignatureConfiguration(string tableName)
        {
            _tableName = tableName;
        }

        public void Configure(EntityTypeBuilder<Signature> builder)
        {
            builder.ToTable(_tableName);
            builder.HasKey(s => s.SignatureId);

            builder.Property(s => s.FormId).IsRequired();
            builder.Property(s => s.UserId).IsRequired();
            builder.Property(s => s.Role)
                .IsRequired()
                .HasConversion(
                    role => role.Value,
                    value => Roles.FromValue(value));
            builder.Property(s => s.Memo).HasMaxLength(500);
            builder.Property(s => s.IsApproved).IsRequired();
            builder.Property(s => s.ApprovedAt);
            builder.Property(s => s.IsRejected).IsRequired();
            builder.Property(s => s.RejectedAt);
        }
    }
}
