using App.Domain.FeatureToggles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Domain.Models.Configurations
{
    public class FeatureToggleValueConfigurations : IEntityTypeConfiguration<FeatureToggleValue>
    {
        public void Configure(EntityTypeBuilder<FeatureToggleValue> builder)
        {
            builder
                .HasOne(t => t.Environment);

            builder
               .HasOne(t => t.FeatureToggle);

            builder
                .Property(p => p.IsEnabled)
                .HasDefaultValue(false);

            builder.Property(t => t.Id).ValueGeneratedNever();
        }
    }
}
