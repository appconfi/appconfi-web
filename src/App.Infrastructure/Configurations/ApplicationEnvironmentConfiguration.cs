using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Domain.Models.Configurations
{
    public class ApplicationEnvironmentConfigurations : IEntityTypeConfiguration<ApplicationEnvironment>
    {
        public void Configure(EntityTypeBuilder<ApplicationEnvironment> builder)
        {
            builder
                .HasOne(t => t.Application)
                .WithMany(t => t.Environments);

            builder.Property(t => t.Id).ValueGeneratedNever();
        }

    }
}
