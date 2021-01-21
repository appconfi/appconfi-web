using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Domain.Models.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(t => t.LastName)
               .IsRequired()
               .HasMaxLength(255);
        }
    }
}
