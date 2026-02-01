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

public sealed class JwtAccessTokenService(
        IClaimsService<AccessAuthInfo> claimsService,
        IJwtTokenService jwtTokenService,
        IOptions<AuthOptions> identityInfrustructureOptions)
        : ITokenService<AccessAuthInfo>
{
    private readonly AuthOptions _indeityConfig = identityInfrustructureOptions.Value;

    public string Create(AccessAuthInfo value)
    {
        DateTime expires = DateTime.UtcNow.AddMinutes(_indeityConfig.ExpireAccessTokenSeconds);
        IEnumerable<Claim> claims = claimsService.Create(value);
        SigningCredentials signingCredentials = jwtTokenService.CreateSigningCredentials(_indeityConfig.AuthSecret);

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
            .GetValidationResultAsync(
                token,
                _indeityConfig.AuthSecret,
                _indeityConfig.ValidAudience,
                _indeityConfig.ValidIssuer,
                checkValidateAudience: _indeityConfig.IsCheckValidAudience,
                checkValidateIssuer: _indeityConfig.IsCheckValidIssuer);

        return result.IsValid;
    }

    public AccessAuthInfo Read(string token)
    {
        JwtSecurityToken jwtSecurityToken = jwtTokenService.ReadJwtToken(token);

        AccessAuthInfo jwtAccessTokenDto = new()
        {
            UserId = jwtSecurityToken.Claims.FindByType(ClaimTypes.NameIdentifier) ?? "",
            Id = jwtSecurityToken.Claims.FindByType(JwtRegisteredClaimNames.Jti) ?? "",
            SessionId = jwtSecurityToken.Claims.FindByType(JwtRegisteredClaimNames.Sid) ?? ""
        };

        return jwtAccessTokenDto;
    }
}
