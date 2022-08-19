using Challenge.Services.ViewModels;
using Challenge.Services.DTO;

namespace Challenge.Services.Utilities;

public static class Responses
{
    public static ResultViewModel ApplicationErrorMessage()
    {
        return new ResultViewModel
        {
            Message = "Ocorreu algum erro interno na aplicação.",
            Success = false,
            Data = null
        };
    }
    
    public static ResultViewModel DomainErrorMessage(string message)
    {
        return new ResultViewModel
        {
            Message = message,
            Success = false,
            Data = null
        };
    }
    
    public static ResultViewModel DomainErrorMessage(string message, IReadOnlyCollection<string> errors)
    {
        return new ResultViewModel
        {
            Message = message,
            Success = false,
            Data = errors
        };
    }
    public static ResultViewModel NotFoundMessage()
    {
        return new ResultViewModel
        {
            Message = "Entidade não encontrada.",
            Success = false,
            Data = null
        };
    }


    public static ResultViewModel EntityFoundMessage<T>(T dto)
    {
        return new ResultViewModel
        {
            Message = "Entidade encontrada.",
            Success = true,
            Data = dto
        };
    }

    public static ResultViewModel? EntityListFoundMessage<T>(List<T> allDto)
    {
        return new ResultViewModel
        {
            Message = "Entidades encontradas!",
            Success = true,
            Data = allDto
        };
    }

    public static ResultViewModel BriefMessage(ResumoDto resumoDto)
    {
        return new ResultViewModel
        {
            Message = "Resumo feito com sucesso!",
            Success = true,
            Data = resumoDto
        };
    }

    public static ResultViewModel NotAllowedMessage()
    {
        return new ResultViewModel
        {
            Message = "Credenciais incorretas, não foi possível realizar o log in.",
            Success = false,
            Data = null
        };
    }
}