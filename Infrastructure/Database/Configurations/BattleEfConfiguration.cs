using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class BattleEfConfiguration : IEntityTypeConfiguration<BattleEntity>
{
    public void Configure(EntityTypeBuilder<BattleEntity> builder)
    {
        builder.HasOne(x => x.TopUser)
            .WithMany(x => x.BattlesInTop)
            .HasForeignKey(x => x.TopUserId);

        builder.HasOne(x => x.BotUser)
            .WithMany(x => x.BattlesInBot)
            .HasForeignKey(x => x.BotUserId);
    }
}
