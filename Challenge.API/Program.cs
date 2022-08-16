using AutoMapper;
using Challenge.API.AutoMapperMaps;
using Challenge.Infrastructure.Context;
using Challenge.Infrastructure.Interfaces;
using Challenge.Infrastructure.Repositories;
using Challenge.Security;
using Challenge.Services.Interfaces;
using Challenge.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Services

builder.Services.AddScoped<IDespesaService, DespesaService>();
builder.Services.AddScoped<IReceitaService, ReceitaService>();
builder.Services.AddScoped<IResumoService, ResumoService>();

#endregion

#region Repositories

builder.Services.AddScoped<IDespesasRepository, DespesasRepository>();
builder.Services.AddScoped<IReceitasRepository, ReceitasRepository>();

#endregion

#region AutoMapper
var mappingConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

#endregion

#region Database

var defaultConnection = builder.Configuration["ConnectionStrings:DefaultConnection"];
var authConnection = builder.Configuration["ConnectionStrings:AuthConnection"];
builder.Services.AddDbContext<FinanceContext>(cfg =>
{
    cfg.UseMySql(defaultConnection, ServerVersion.AutoDetect(defaultConnection));
}, ServiceLifetime.Transient);

#endregion

#region Identity

builder.Services.AddDbContext<AuthContext>(cfg =>
{
    cfg.UseMySql(authConnection, ServerVersion.AutoDetect(authConnection));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(opt =>
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
}).AddEntityFrameworkStores<AuthContext>();

#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();