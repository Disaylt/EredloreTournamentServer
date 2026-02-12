using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal class UnitEfConfiguration : IEntityTypeConfiguration<UnitEntity>
{
    public void Configure(EntityTypeBuilder<UnitEntity> builder)
    {
        builder.ComplexCollection(p => p.Abilities, b => b.ToJson());

        builder.HasOne(x => x.UserCollection)
            .WithMany(x => x.Units)
            .HasForeignKey(x => x.UserCollectionId);
    }
}
