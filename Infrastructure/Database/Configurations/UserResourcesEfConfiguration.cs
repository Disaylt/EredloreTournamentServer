using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal sealed class UserResourcesEfConfiguration : IEntityTypeConfiguration<UserResourcesEntity>
{
    public void Configure(EntityTypeBuilder<UserResourcesEntity> builder)
    {
        builder.HasKey(x => x.UserId);

        builder.HasOne(x => x.User)
            .WithOne(x => x.Resources)
            .HasForeignKey<UserResourcesEntity>(x => x.UserId);
    }
}
