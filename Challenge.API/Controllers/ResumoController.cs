using Challenge.Services.Utilities;
using Challenge.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.API.Controllers;

[ApiController]
[Authorize]
public class ResumoController : Controller
{
    private readonly IResumoService _resumoService;

    public ResumoController(IResumoService resumoService)
    {
        _resumoService = resumoService;
    }

    [HttpGet]
    [Route("api/v1/resumo/{ano}/{mes}")]
    public async Task<IActionResult> GetResume([FromRoute] int ano, int mes)
    {
        try
        {
            var resumo = await _resumoService.ResumoOverall(ano, mes);
            return Ok(Responses.BriefMessage(resumo));
        }
        catch (Exception e)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage());
        }
    }
}