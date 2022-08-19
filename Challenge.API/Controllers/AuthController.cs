using System.Security.Authentication;
using Challenge.Security.Interfaces;
using Challenge.Security.ViewModels;
using Challenge.Services;
using Challenge.Services.Utilities;
using Challenge.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.API.Controllers;

[ApiController]
public class AuthController : Controller
{
    private readonly ITokenManager _tokenManager;
    private readonly IAuthServices _authServices;
    public AuthController(ITokenManager tokenManager, IAuthServices authServices)
    {
        _tokenManager = tokenManager;
        _authServices = authServices;
    }

    [HttpPost]
    [Route("/api/v1/auth/register")]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel request)
    {
        try
        {
            if (!ModelState.IsValid)
                return StatusCode(406, "Email ou senha não válidos.");
            
            var user = await _authServices.RegisterAsync(request);
            return Ok(new ResultViewModel
            {
                Message = "Registro efetuado com sucesso!",
                Success = true,
            });
        }
        catch (ServiceException e)
        {
            return BadRequest(new ResultViewModel
            {
                Message = e.Message,
                Success = false,
                Data = null
            });
        }
        catch (Exception)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage());
        }
    }

    [HttpPost]
    [Route("/api/v1/auth/login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel? request)
    {
        try
        {
            if (!ModelState.IsValid)
                return StatusCode(406, "Email ou senha não válidos.");
            
            var existingUser = await _authServices.AuthenticateAsync(request);
            return Ok(new ResultViewModel
            {
                Message = "Log in realizado com sucesso.",
                Success = true,
                Data = new AuthToken
                {
                    Token = await _tokenManager.GenerateToken(existingUser),
                    TokenType = "Bearer"
                }
            });
        }
        catch (Exception e) when (e is AuthenticationException || e is ServiceException)
        {
            return Unauthorized(new ResultViewModel
            {
                Message = e.Message,
                Success = false,
                Data = null
            });
        }
        catch (Exception)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage());
        }
    }
}