using Challenge.Domain.Enums;

namespace Challenge.Services.DTO;

public class ResumoDTO
{
    public ResumoDTO(decimal receitaValue, decimal despesaValue, decimal saldo, List<CategoriaDTO> categoriaOverAll)
    {
        ReceitaValue = receitaValue;
        DespesaValue = despesaValue;
        Saldo = saldo;
        CategoriaOverAll = categoriaOverAll;
    }

    public ResumoDTO()
    {
    }

    public decimal ReceitaValue { get; set; }
    public decimal DespesaValue { get; set; }
    public decimal Saldo { get; set; }
    public List<CategoriaDTO> CategoriaOverAll { get; set; }
}