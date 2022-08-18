using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Challenge.Security.Jwt;

public class TokenSettings
{
    public ClaimsIdentity Subject { get; private set; }
    public string Audience { get; }
    public string Issuer { get; }
    public int ValidForMinutes { get; }
    public SigningCredentials SigningCredentials { get; }
    public DateTime IssuedAt => DateTime.UtcNow;
    public DateTime NotBefore => IssuedAt;
    public DateTime AccessTokenExpiration => IssuedAt.AddMinutes(ValidForMinutes);

    public TokenSettings(IConfiguration configuration)
    {
        Issuer = configuration["Jwt:Issuer"];
        Audience = configuration["Jwt:Audience"];
        ValidForMinutes = Convert.ToInt32(configuration["Jwt:ValidForMinutes"]);

        var key = configuration["Jwt:Key"];
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        SigningCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);
    }

    public ClaimsIdentity AddSubject(IdentityUser user)
    {
        var claims = new ClaimsIdentity
        (
            new GenericIdentity(user.Email),
            new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName)
            }
        );

        return claims;
    }
}