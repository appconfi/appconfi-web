using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Domain.Models.Configurations
{
    public class ApplicationAccessKeyConfigurations : IEntityTypeConfiguration<ApplicationAccessKey>
    {
        public void Configure(EntityTypeBuilder<ApplicationAccessKey> builder)
        {
            builder
                .HasOne(t => t.Application)
                .WithOne(t => t.AccessKey);

            builder.Property(t => t.Id)
                    .ValueGeneratedOnAdd();
        }
    }
}
