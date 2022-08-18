using Challenge.Security.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Challenge.Security.Interfaces;

public interface ITokenManager
{
    Task<AuthToken> GenerateToken(IdentityUser user);
}