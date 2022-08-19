using Challenge.Security.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Challenge.Security;

public class SeedUserRoleInitial : ISeedUserRoleInitial
{
    private readonly UserManager<IdentityUser<long>> _userManager;
    private readonly RoleManager<IdentityRole<long>> _roleManager;

    public SeedUserRoleInitial(UserManager<IdentityUser<long>> userManager, RoleManager<IdentityRole<long>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void SeedUsers()
    {
        if (_userManager.FindByEmailAsync("administrator@securityenvironment.com").Result == null)
        {
            IdentityUser<long> user = new();
            user.UserName = "Administrator";
            user.Email = "administrator@securityenvironment.com";
            user.NormalizedUserName = "ADMINISTRATOR";
            user.NormalizedEmail = "ADMINISTRATOR@SECURITYENVIRONMENT.COM";
            user.EmailConfirmed = true;
            user.SecurityStamp = Guid.NewGuid().ToString();

            var result = _userManager.CreateAsync(user, "qVwfciK8@").Result;
            if (result.Succeeded)
                _userManager.AddToRoleAsync(user, "Admin").Wait();
        }

        if (_userManager.FindByEmailAsync("initialuser@securityenvironment.com").Result == null)
        {
            IdentityUser<long> user = new();
            user.UserName = "InitialUser";
            user.Email = "initialuser@securityenvironment.com";
            user.NormalizedUserName = "INITIALUSER";
            user.NormalizedEmail = "INITIALUSER@SECURITYENVIRONMENT.COM";
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            user.SecurityStamp = Guid.NewGuid().ToString();

            var result = _userManager.CreateAsync(user, "qVwfciK8@").Result;
            
            if (result.Succeeded)
                _userManager.AddToRoleAsync(user, "User").Wait();
        }
    }

    public void SeedRoles()
    {
        if (!_roleManager.RoleExistsAsync("Admin").Result)
        {
            IdentityRole<long> role = new();
            role.Name = "Admin";
            role.NormalizedName = "ADMIN";
            var roleResult = _roleManager.CreateAsync(role).Result;
        }

        if (!_roleManager.RoleExistsAsync("User").Result)
        {
            IdentityRole<long> role = new();
            role.Name = "User";
            role.NormalizedName = "USER";
            var roleResult = _roleManager.CreateAsync(role).Result;
        }
    }
}