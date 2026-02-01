using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Domain.Common;
using Infrastructure.Features.Auth.Services.Abstraction;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Features.Auth.Services.Implementation;

public sealed class JwtTokenService(
    IJwtSecurityTokenHandler jwtSecurityTokenHandler,
    ILogger<JwtTokenService> logger)
    : IJwtTokenService
{
    public SigningCredentials CreateSigningCredentials(string secret)
    {
        byte[] secretBytes = Encoding.UTF8.GetBytes(secret);
        SymmetricSecurityKey authSigningKey = new(secretBytes);

        return new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);
    }

    public async Task<TokenValidationResult> GetValidationResultAsync(
        string token,
        string secret,
        string? validAudience,
        string? validIssuer,
        bool checkIssuerSigningKey = true,
        bool checkValidateIssuer = true,
        bool checkValidateAudience = true,
        bool checkValidateLifetime = true)
    {
        HMACSHA256 hmac = new(Encoding.UTF8.GetBytes(secret));

        TokenValidationParameters tokenValidationParameters = new()
        {
            ValidateIssuerSigningKey = checkIssuerSigningKey,
            IssuerSigningKey = new SymmetricSecurityKey(hmac.Key),
            ValidateIssuer = checkValidateIssuer,
            ValidateAudience = checkValidateAudience,
            ValidateLifetime = checkValidateLifetime,
            ValidAudience = validAudience,
            ValidIssuer = validIssuer,
            ClockSkew = TimeSpan.Zero,
        };
        return await jwtSecurityTokenHandler.ValidateTokenAsync(token, tokenValidationParameters);
    }

    public JwtSecurityToken ReadJwtToken(string token)
    {
        try
        {
            return jwtSecurityTokenHandler.ReadJwtToken(token);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unable to read jwt token - {token}", token);

            throw new CoreRequestException()
                .AddMessages(["Не удалось прочить токен"]);
        }
    }
}
