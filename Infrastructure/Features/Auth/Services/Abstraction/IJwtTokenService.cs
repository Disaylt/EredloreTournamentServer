using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Features.Auth.Services.Abstraction;

public interface IJwtTokenService
{
    public SigningCredentials CreateSigningCredentials(string secret);
    public JwtSecurityToken ReadJwtToken(string token);
    public Task<TokenValidationResult> GetValidationResultAsync
    (string token,
            string secret,
            string? validAudience,
            string? validIssuer,
            bool checkIssuerSigningKey = true,
            bool checkValidateIssuer = true,
            bool checkValidateAudience = true,
            bool checkValidateLifetime = true);
}
