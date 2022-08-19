using Challenge.IoC;
using Challenge.Security.Interfaces;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region Swagger Documentation

var securityScheme = new OpenApiSecurityScheme
{
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "JWT Authentication for Challenge API"
};

var securityRequirements = new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] { }
    }
};

var contactInfo = new OpenApiContact
{
    Name = "Victor Teixeira",
    Email = "vicktorteixeira.7@gmail.com",
    Url = new Uri("https://github.com/victrteixeira")
};

var license = new OpenApiLicense
{
    Name = "Free License"
};

var info = new OpenApiInfo
{
    Version = "v1",
    Title = "Back-End Challenge from Alura to Familiar Financial Control.",
    Contact = contactInfo,
    License = license
};

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", info);
    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(securityRequirements);
});
#endregion

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddSecurity(builder.Configuration);
builder.Services.AddJwtBearer(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();