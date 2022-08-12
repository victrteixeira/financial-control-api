using AutoMapper;
using Challenge.API.Utilities;
using Challenge.API.ViewModels;
using Challenge.Core;
using Challenge.Services.DTO;
using Challenge.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.API.Controllers;

[ApiController]
public class DespesaController : Controller
{
    private readonly IMapper _mapper;
    private readonly IDespesaService _despesaService;
    
    public DespesaController(IMapper mapper, IDespesaService despesaService)
    {
        _mapper = mapper;
        _despesaService = despesaService;
    }
    
    [HttpPost]
    [Route("api/v1/despesas/create")]
    public async Task<IActionResult> Create([FromBody] CreateDespesaViewModel createViewModel)
    {
        try
        {
            var despesaDto = _mapper.Map<DespesasDTO>(createViewModel);
            var despesaCreated = await _despesaService.CreateAsync(despesaDto);
            return StatusCode(201, new ResultViewModel
            {
                Message = "Despesa criada com sucesso!",
                Success = true,
                Data = despesaCreated
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
    [Route("api/v1/despesas/update")]
    public async Task<IActionResult> Update([FromBody] UpdateDespesaViewModel updateViewModel)
    {
        try
        {
            var newDespesaDto = _mapper.Map<DespesasDTO>(updateViewModel);
            var despesaUpdated = await _despesaService.UpdateAsync(newDespesaDto);
            return Ok(new ResultViewModel
            {
                Message = "Despesa atualizada com sucesso",
                Success = true,
                Data = despesaUpdated
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
    [Route("api/v1/despesas/remove/{id}")]
    public async Task<IActionResult> Remove(long id)
    {
        try
        {
            await _despesaService.RemoveAsync(id);
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
    [Route("api/v1/despesas/{id}")]
    public async Task<IActionResult> GetDespesa(long id)
    {
        try
        {
            var despesaFound = await _despesaService.GetAsync(id);
            return Ok(Responses.EntityFoundMessage(despesaFound));
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
    [Route("api/v1/despesas")]
    public async Task<IActionResult> GetAll([FromQuery] string? descricao = null)
    {
        try
        {
            if (string.IsNullOrEmpty(descricao))
            {
                var allReceitas = await _despesaService.GetAllAsync();
                return Ok(Responses.EntityListFoundMessage(allReceitas));
            }

            var searchResult = await _despesaService.SearchByDescricaoAsync(descricao);
            return Ok(Responses.EntityListFoundMessage(searchResult));

        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    
    [HttpGet]
    [Route("api/v1/despesas/{ano}/{mes}")]
    public async Task<IActionResult> ListByMonth([FromRoute] int ano, int mes)
    {
        try
        {
            var searchResult = await _despesaService.GetByMesAsync(ano, mes);
            return Ok(Responses.EntityListFoundMessage(searchResult));
        }
        catch (Exception e)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage());
        }
    }
}