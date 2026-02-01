using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Features.Auth.Models;
using Infrastructure.Features.Auth.Services.Abstraction;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Features.Auth.Services.Implementation;

public sealed class JwtRefreshClaimsService : IClaimsService<RefreshAuthInfo>
{
    public ICollection<Claim> Create(RefreshAuthInfo details)
    {
        Claim jti = new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
        Claim iat = new(JwtRegisteredClaimNames.Iat,
            EpochTime.GetIntDate(DateTime.UtcNow).ToString(CultureInfo.InvariantCulture),
            ClaimValueTypes.Integer64);
        Claim sessionId = new(ClaimTypes.Sid, details.SessionId);

        List<Claim> claims =
        [
            jti,
            iat,
            sessionId
        ];

        return claims;
    }
}
