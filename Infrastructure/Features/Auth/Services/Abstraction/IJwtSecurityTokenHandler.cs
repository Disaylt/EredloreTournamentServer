using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Features.Auth.Services.Abstraction;

public interface IJwtSecurityTokenHandler
{
    JwtSecurityToken ReadJwtToken(string token);
    Task<TokenValidationResult> ValidateTokenAsync(string token, TokenValidationParameters validationParameters);
}
