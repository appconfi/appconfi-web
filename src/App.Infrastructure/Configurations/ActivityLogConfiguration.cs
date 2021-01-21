using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Domain.Models.Configurations
{
    public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
    {
        public void Configure(EntityTypeBuilder<ActivityLog> builder)
        {
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(p => p.Application);

            builder.HasIndex(x => x.ApplicationId);

            builder.HasOne(p => p.InitiatedBy);

            builder.Property(t => t.Id).ValueGeneratedNever();
        }
    }
}
