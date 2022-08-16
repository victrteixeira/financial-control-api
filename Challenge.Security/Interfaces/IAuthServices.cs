using Challenge.Security.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Challenge.Security.Interfaces;

public interface IAuthServices
{
    Task<IdentityResult> RegisterAsync(RegisterViewModel request);
    Task<SuccessfulLoginResponse> LoginAsync(LoginViewModel request);
}