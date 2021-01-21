using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Domain.Models.Configurations
{
    public class UserApplicationConfigurations : IEntityTypeConfiguration<UserApplication>
    {
        public void Configure(EntityTypeBuilder<UserApplication> builder)
        {
            builder
                .HasOne(t => t.Application)
                .WithMany(t => t.Users);
            builder
                .HasOne(t => t.User);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

        }
    }
}
