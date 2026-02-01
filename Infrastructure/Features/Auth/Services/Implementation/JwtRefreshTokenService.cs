using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Features.Auth.Models;
using Application.Features.Auth.Services.Abstraction;
using Infrastructure.Features.Auth.Extensions;
using Infrastructure.Features.Auth.Services.Abstraction;
using Infrastructure.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Features.Auth.Services.Implementation;

public sealed class JwtRefreshTokenService(
        IClaimsService<RefreshAuthInfo> claimsService,
        IJwtTokenService jwtTokenService,
        IOptions<AuthOptions> identityInfrustructureOptions)
        : ITokenService<RefreshAuthInfo>
{
    private readonly AuthOptions _indeityConfig = identityInfrustructureOptions.Value;

    public string Create(RefreshAuthInfo value)
    {
        DateTime expires = DateTime.UtcNow.AddHours(_indeityConfig.ExpireRefreshTokenHours);
        IEnumerable<Claim> claims = claimsService.Create(value);
        SigningCredentials signingCredentials = jwtTokenService.CreateSigningCredentials(_indeityConfig.RefreshTokenSecret);

        JwtSecurityToken jwtSecurityToken = new(
            _indeityConfig.ValidIssuer,
            _indeityConfig.ValidAudience,
            claims,
            expires: expires,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(jwtSecurityToken);
    }

    public async Task<bool> IsValid(string token)
    {
        TokenValidationResult result = await jwtTokenService
        .GetValidationResultAsync(token,
            _indeityConfig.RefreshTokenSecret,
            _indeityConfig.ValidAudience,
            _indeityConfig.ValidIssuer,
            checkValidateAudience: _indeityConfig.IsCheckValidAudience,
            checkValidateIssuer: _indeityConfig.IsCheckValidIssuer);

        return result.IsValid;
    }

    public RefreshAuthInfo Read(string token)
    {
        JwtSecurityToken jwtSecurityToken = jwtTokenService.ReadJwtToken(token);

        return new RefreshAuthInfo
        {
            SessionId = jwtSecurityToken.Claims.FindByType(ClaimTypes.Sid) ?? "",
        };
    }
}
