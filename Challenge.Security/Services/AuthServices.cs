using System.Security.Authentication;
using Challenge.Core;
using Challenge.Security.Interfaces;
using Challenge.Security.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Challenge.Security.Services;

public class AuthServices : IAuthServices
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthServices(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }


    public async Task<IdentityResult> RegisterAsync(RegisterViewModel request)
    {
        var existingEmail = await _userManager.FindByEmailAsync(request.Email);
        if (request.Password != request.ConfirmPassword || existingEmail != null)
            return IdentityResult.Failed(new IdentityError { Description = "Credentials don't match or email already in use." });
        
        var userIdentity = new IdentityUser
        {
            Email = request.Email,
            UserName = request.UserName
        };

        var isCreated = _userManager.CreateAsync(userIdentity, request.Password).Result;
        await _userManager.AddToRoleAsync(userIdentity, "User");

        return isCreated;
    }

    public async Task<SuccessfulLoginResponse> LoginAsync(LoginViewModel request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        var checkPassword = await _userManager.CheckPasswordAsync(existingUser, request.Password);
        
        if(existingUser == null || !checkPassword)
            return IdentityResult.Failed(new IdentityError{ Description = "Credentials incorrect." });
        
        

    }
}