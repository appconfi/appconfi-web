using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Domain.Models.Configurations
{
    public class TargetPercentageConfigurations : IEntityTypeConfiguration<TargetPercentage>
    {
        public void Configure(EntityTypeBuilder<TargetPercentage> builder)
        {
            builder.HasBaseType<TargetRule>();

            builder.OwnsOne(x => x.Percent).Property(x => x.Number).HasColumnName("Percent");

        }
    }
}
