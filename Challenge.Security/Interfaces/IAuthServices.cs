using Challenge.Security.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Challenge.Security.Interfaces;

public interface IAuthServices
{
    Task<IdentityUser<long>?> RegisterAsync(RegisterViewModel? request);
    Task<IdentityUser<long>?> AuthenticateAsync(LoginViewModel? request);
}