using AutoMapper;
using Challenge.API.AutoMapperMaps;
using Challenge.Infrastructure.Context;
using Challenge.Infrastructure.Interfaces;
using Challenge.Infrastructure.Repositories;
using Challenge.Services.Interfaces;
using Challenge.Services.Services;
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

var connString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<FinanceContext>(cfg =>
{
    cfg.UseMySql(connString, ServerVersion.AutoDetect(connString));
}, ServiceLifetime.Transient);

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

app.UseAuthorization();

app.MapControllers();

app.Run();