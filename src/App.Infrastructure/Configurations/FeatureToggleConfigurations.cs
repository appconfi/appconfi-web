using App.Domain.FeatureToggles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Domain.Models.Configurations
{
    public class FeatureToggleConfigurations : IEntityTypeConfiguration<FeatureToggle>
    {
        public void Configure(EntityTypeBuilder<FeatureToggle> builder)
        {
            builder
                .HasOne(t => t.Application)
                .WithMany(t => t.FeatureToggles);

            builder
                .Property(p => p.Key)
                .HasMaxLength(256);

            builder.Property(t => t.Id).ValueGeneratedNever();
        }
    }
}
