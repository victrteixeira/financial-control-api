using Microsoft.AspNetCore.Identity;

namespace Challenge.Security.Interfaces;

public interface ITokenManager
{
    Task<string> GenerateToken(IdentityUser<long>? user);
}