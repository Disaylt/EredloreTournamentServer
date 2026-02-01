using System.IdentityModel.Tokens.Jwt;
using Infrastructure.Features.Auth.Services.Abstraction;

namespace Infrastructure.Features.Auth.Services.Implementation;

public class CustomJwtSecurityTokenHandler : JwtSecurityTokenHandler, IJwtSecurityTokenHandler;
