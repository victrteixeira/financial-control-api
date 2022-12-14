using AutoMapper;
using Challenge.Services.Utilities;
using Challenge.Services.ViewModels;
using Challenge.Services;
using Challenge.Services.DTO;
using Challenge.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.API.Controllers;

[ApiController]
[Authorize]
public class ReceitaController : Controller
{
    private readonly IMapper _mapper;
    private readonly IReceitaService _receitaService;
    
    public ReceitaController(IMapper mapper, IReceitaService receitaService)
    {
        _mapper = mapper;
        _receitaService = receitaService;
    }

    [HttpPost]
    [Route("api/v1/receitas/create")]
    public async Task<IActionResult> Create([FromBody] CreateReceitaViewModel createViewModel)
    {
        try
        {
            var receitaDto = _mapper.Map<ReceitasDto>(createViewModel);
            var receitaCreated = await _receitaService.CreateAsync(receitaDto);
            return StatusCode(201, new ResultViewModel
            {
                Message = "Receita criada com sucesso!",
                Success = true,
                Data = receitaCreated
            });
        }
        catch (DomainException e)
        {
            return BadRequest(Responses.DomainErrorMessage(e.Message, e.Errors));
        }
        catch (Exception ex)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage());
        }
    }
    
    [HttpPut]
    [Route("api/v1/receitas/update")]
    public async Task<IActionResult> Update([FromBody] UpdateReceitaViewModel updateViewModel)
    {
        try
        {
            var newReceitaDto = _mapper.Map<ReceitasDto>(updateViewModel);
            var receitaUpdated = await _receitaService.UpdateAsync(newReceitaDto);
            return Ok(new ResultViewModel
            {
                Message = "Despesa atualizada com sucesso",
                Success = true,
                Data = receitaUpdated
            });
        }
        catch (Exception e) when (e is ServiceException || e is DomainException)
        {
            if (e is ServiceException)
                return StatusCode(404, Responses.NotFoundMessage());
            else if (e is DomainException)
                return BadRequest(Responses.DomainErrorMessage(e.Message));
            else
                return StatusCode(500, Responses.ApplicationErrorMessage());
        }
    }
    
    [HttpDelete]
    [Route("api/v1/receitas/remove/{id}")]
    public async Task<IActionResult> Remove(long id)
    {
        try
        {
            await _receitaService.RemoveAsync(id);
            return NoContent();
        }
        catch (ServiceException e)
        {
            return StatusCode(404, e.Message);
        }
        catch(Exception ex)
        {
            return BadRequest(Responses.ApplicationErrorMessage());
        }
    }
    
    [HttpGet]
    [Route("api/v1/receitas/{id}")]
    public async Task<IActionResult> GetReceita(long id)
    {
        try
        {
            var receitaFound = await _receitaService.GetAsync(id);
            return Ok(Responses.EntityFoundMessage(receitaFound));
        }
        catch (ServiceException e)
        {
            return StatusCode(404, e.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(Responses.ApplicationErrorMessage());
        }
    }
    
    [HttpGet]
    [Route("api/v1/receitas")]
    public async Task<IActionResult> GetAll([FromQuery] string? descricao = null)
    {
        try
        {
            if (string.IsNullOrEmpty(descricao))
            {
                var list = await _receitaService.GetAllAsync();
                return Ok(Responses.EntityListFoundMessage(list));
            }

            var searchResult = await _receitaService.SearchByDescricaoAsync(descricao);
            return Ok(Responses.EntityListFoundMessage(searchResult));
        }
        catch (Exception ex)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage());
        }
    }
    
    [HttpGet]
    [Route("api/v1/receitas/{ano}/{mes}")]
    public async Task<IActionResult> ListByMonth([FromRoute] int ano, int mes)
    {
        try
        {
            var searchResult = await _receitaService.GetByMesAsync(ano, mes);
            return Ok(Responses.EntityListFoundMessage(searchResult));
        }
        catch (Exception e)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage());
        }
    }
}