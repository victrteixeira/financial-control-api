using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Challenge.Security.Interfaces;
using Challenge.Security.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Challenge.Security.Jwt;

public class TokenManager : ITokenManager
{
    private readonly TokenSettings _tokenSettings;

    public TokenManager(TokenSettings tokenSettings)
    {
        _tokenSettings = tokenSettings;
    }

    public async Task<AuthToken> GenerateToken(IdentityUser user)
    {
        var handler = new JwtSecurityTokenHandler();
        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = _tokenSettings.AddSubject(user),
            Issuer = _tokenSettings.Issuer,
            Audience = _tokenSettings.Audience,
            IssuedAt = _tokenSettings.IssuedAt,
            NotBefore = _tokenSettings.NotBefore,
            Expires = _tokenSettings.AccessTokenExpiration,
            SigningCredentials = _tokenSettings.SigningCredentials
        };

        var token = handler.CreateToken(tokenDescription);
        var accessToken = handler.WriteToken(token);

        return new AuthToken
        {
            Message = "Successful Log in",
            Token = accessToken,
            TokenType = "Bearer",
            ExpiresIn = (long)TimeSpan.FromMinutes(_tokenSettings.ValidForMinutes).TotalSeconds
        };
    }
}