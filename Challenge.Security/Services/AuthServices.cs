using System.Security.Authentication;
using Challenge.Security.Interfaces;
using Challenge.Security.ViewModels;
using Challenge.Services;
using Microsoft.AspNetCore.Identity;

namespace Challenge.Security.Services;

public class AuthServices : IAuthServices
{
    private readonly UserManager<IdentityUser<long>> _userManager;
    private readonly SignInManager<IdentityUser<long>> _signInManager;
    private readonly ITokenManager _tokenManager;

    public AuthServices(UserManager<IdentityUser<long>> userManager, SignInManager<IdentityUser<long>> signInManager, ITokenManager tokenManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenManager = tokenManager;
    }

    public async Task<IdentityUser<long>?> RegisterAsync(RegisterViewModel? request)
    {
        if (request != null)
        {
            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (request.Password != request.ConfirmPassword)
                throw new ServiceException("As senhas não são idênticas.");
            if (existingEmail != null)
                throw new ServiceException("Email já em utilização.");
        }

        var userIdentity = new IdentityUser<long>
        {
            Email = request?.Email,
            UserName = request?.UserName
        };
        
        var isCreated = await _userManager.CreateAsync(userIdentity, request?.Password);
        if (isCreated.Succeeded)
            await _userManager.AddToRoleAsync(userIdentity, "User");

        return userIdentity;
    }

    public async Task<IdentityUser<long>?> AuthenticateAsync(LoginViewModel? request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request?.Email);
        if (existingUser == null)
            throw new ServiceException("Nenhum usuário encontrado com estas credenciais.");
        
        var checkPassword = await _signInManager.CheckPasswordSignInAsync(existingUser, request?.Password, false);
        if (checkPassword.Succeeded)
            return existingUser;
        
        throw new AuthenticationException("Email ou senhas incorretos.");
    }
}