using Challenge.Security.Interfaces;
using Challenge.Security.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Challenge.Security.Services;

public class AuthServices : IAuthServices
{
    private readonly UserManager<IdentityUser<long>> _userManager;

    public AuthServices(UserManager<IdentityUser<long>> userManager)
    {
        _userManager = userManager;
    }


    public async Task<IdentityResult> RegisterAsync(RegisterViewModel request)
    {
        var existingEmail = await _userManager.FindByEmailAsync(request.Email);
        if (request.Password != request.ConfirmPassword || existingEmail != null)
            return IdentityResult.Failed(new IdentityError { Description = "Credentials don't match or email already in use." });
        
        var userIdentity = new IdentityUser<long>
        {
            Email = request.Email,
            UserName = request.UserName
        };

        var isCreated = _userManager.CreateAsync(userIdentity, request.Password).Result;
        await _userManager.AddToRoleAsync(userIdentity, "User");

        return isCreated;
    }

    public async Task<IdentityResult> LoginAsync(LoginViewModel request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        var checkPassword = await _userManager.CheckPasswordAsync(existingUser, request.Password);

        if (existingUser == null || !checkPassword)
            return IdentityResult.Failed(new IdentityError 
                { Description = "Those credentials don't match with any account." });

        return IdentityResult.Success;
    }
}