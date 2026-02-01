using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class AreploreTournamentDbContext : IdentityDbContext<UserEntity>
{
    public AreploreTournamentDbContext(DbContextOptions<AreploreTournamentDbContext> options) : base(options)
    {

    }

    public DbSet<SessionEntity> Sessions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
