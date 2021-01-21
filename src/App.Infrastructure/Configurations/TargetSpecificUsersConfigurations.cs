using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Domain.Models.Configurations
{
    public class TargetSpecificUsersConfigurations : IEntityTypeConfiguration<TargetSpecificUsers>
    {
        public void Configure(EntityTypeBuilder<TargetSpecificUsers> builder)
        {
            builder.HasBaseType<TargetRule>();
            builder.OwnsOne(x => x.ValuesList).Property(x => x.List).HasColumnName("ValueList");
        }
    }
}
