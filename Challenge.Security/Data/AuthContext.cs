using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Security;

public class AuthContext : IdentityDbContext<IdentityUser<long>, IdentityRole<long>, long>
{
    public AuthContext(DbContextOptions<AuthContext> options) : base(options)
    {
    }

    public AuthContext()
    {
    }
}