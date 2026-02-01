using Application.Features.Auth.Interfaces;

namespace Application.Features.Auth.Services.Abstraction;

public interface ITokenService<TTokenData> where TTokenData : class, IAuthInfo
{
    public string Create(TTokenData value);
    public TTokenData Read(string token);
    public Task<bool> IsValid(string token);
}
