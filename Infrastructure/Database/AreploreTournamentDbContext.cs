using Domain.Entities;
using Infrastructure.Database.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class AreploreTournamentDbContext : IdentityDbContext<UserEntity>
{
    public AreploreTournamentDbContext(DbContextOptions<AreploreTournamentDbContext> options) : base(options)
    {

    }

    public DbSet<SessionEntity> Sessions { get; set; } = null!;
    public DbSet<BattleEntity> Battles { get; set; } = null!;
    public DbSet<CollectionEntity> Collectons { get; set; } = null!;
    public DbSet<UnitEntity> Units { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BattleEfConfiguration());
        modelBuilder.ApplyConfiguration(new CollectionEfConfiguration());
        modelBuilder.ApplyConfiguration(new UnitEfConfiguration());
        modelBuilder.ApplyConfiguration(new UserResourcesEfConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
