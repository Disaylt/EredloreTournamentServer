using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Features.Auth.Models;
using Infrastructure.Features.Auth.Services.Abstraction;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Features.Auth.Services.Implementation;

public sealed class JwtAccessClaimsService : IClaimsService<AccessAuthInfo>
{
    public ICollection<Claim> Create(AccessAuthInfo details)
    {
        Claim jti = new(JwtRegisteredClaimNames.Jti, details.Id);
        Claim iat = new(JwtRegisteredClaimNames.Iat,
            EpochTime.GetIntDate(DateTime.UtcNow).ToString(CultureInfo.InvariantCulture),
            ClaimValueTypes.Integer64);
        Claim userId = new(ClaimTypes.NameIdentifier, details.UserId);
        Claim sessionId = new(ClaimTypes.Sid, details.SessionId);

        List<Claim> claims =
        [
            jti,
            iat,
            userId,
            sessionId
        ];

        return claims;
    }
}
