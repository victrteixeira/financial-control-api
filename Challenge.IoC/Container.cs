using AutoMapper;
using Challenge.Infrastructure.Context;
using Challenge.Infrastructure.Interfaces;
using Challenge.Infrastructure.Repositories;
using Challenge.Security;
using Challenge.Security.Interfaces;
using Challenge.Security.Jwt;
using Challenge.Security.Services;
using Challenge.Services.AutoMapperMaps;
using Challenge.Services.Interfaces;
using Challenge.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Challenge.IoC;

public static class Container
{
    public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
    {
        // JWT
        services.AddScoped<ITokenManager, TokenManager>();
        services.AddScoped<TokenSettings>();
        var jwtSettings = services.BuildServiceProvider().GetRequiredService<TokenSettings>();

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwtOpt => new TokenValidationParameters
        {
            ValidateActor = false,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = jwtSettings.SigningCredentials.Key
        });
        
        // Identity
        var authConnection = configuration["ConnectionStrings:AuthConnection"];

        services.AddDbContext<AuthContext>(cfg =>
        {
            cfg.UseMySql(authConnection, ServerVersion.AutoDetect(authConnection));
        });

        services.AddIdentity<IdentityUser<long>, IdentityRole<long>>(opt =>
        {
            opt.SignIn.RequireConfirmedAccount = true;
            opt.Password.RequireDigit = true;
            opt.Password.RequiredLength = 8;
            opt.Password.RequireLowercase = true;
            opt.Password.RequireUppercase = true;
            opt.Password.RequireNonAlphanumeric = true;
            opt.Password.RequiredUniqueChars = 1;

            opt.User.RequireUniqueEmail = true;
            opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        }).AddEntityFrameworkStores<AuthContext>().AddDefaultTokenProviders();
        services.AddScoped<IAuthServices, AuthServices>();

        services.AddAuthorization();
        return services;
    }
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDespesasRepository, DespesasRepository>();
        services.AddScoped<IReceitasRepository, ReceitasRepository>();

        var defaultConnection = configuration["ConnectionStrings:DefaultConnection"];
        services.AddDbContext<FinanceContext>(cfg =>
        {
            cfg.UseMySql(defaultConnection, ServerVersion.AutoDetect(defaultConnection));
        }, ServiceLifetime.Transient);
        
        var mappingConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        var mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);
        

        return services;
    }
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDespesaService, DespesaService>();
        services.AddScoped<IReceitaService, ReceitaService>();
        services.AddScoped<IResumoService, ResumoService>();

        return services;
    }
}

