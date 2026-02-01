using Domain.Entities;

namespace Application.Features.Users.Services.Abstraction;

public interface IUserService
{
    Task<UserEntity> FindByEmailAsync(string email);
    Task<UserEntity> CreateAsync(string email, string password);
    Task<bool> VerifyPasswordAsync(string email, string password);
    Task DeleteAsync(string id);
}
