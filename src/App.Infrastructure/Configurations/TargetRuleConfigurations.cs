using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Domain.Models.Configurations
{
    public class TargetRuleConfigurations : IEntityTypeConfiguration<TargetRule>
    {
        public void Configure(EntityTypeBuilder<TargetRule> builder)
        {
            builder
                .HasOne(t => t.UserTargeting)
                .WithOne(t => t.TargetRule);

            builder.Property(t => t.Id)
                    .ValueGeneratedOnAdd();
        }
    }
}
