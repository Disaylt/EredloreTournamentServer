using System.Security.Claims;
using Application.Features.Auth.Interfaces;

namespace Infrastructure.Features.Auth.Services.Abstraction;

public interface IClaimsService<in TTokenData> where TTokenData : class, IAuthInfo
{
    ICollection<Claim> Create(TTokenData details);
}
