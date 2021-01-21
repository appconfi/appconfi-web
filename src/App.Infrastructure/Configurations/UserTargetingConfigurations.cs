using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Domain.Models.Configurations
{
    public class UserTargetingConfigurations : IEntityTypeConfiguration<UserTargeting>
    {
        public void Configure(EntityTypeBuilder<UserTargeting> builder)
        {
            builder
                .HasOne(t => t.FeatureToggle);

            builder
                .HasOne(t => t.Environment);

            builder
                .HasOne(t => t.TargetRule)
                .WithOne(r => r.UserTargeting);
        }
    }
}
