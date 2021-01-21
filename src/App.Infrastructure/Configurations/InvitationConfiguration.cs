using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Domain.Models.Configurations
{
    public class InvitationConfigurations : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(x => x.Application);
        }
    }
}
