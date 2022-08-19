namespace Challenge.Services.DTO;

public class ResumoDto
{
    public ResumoDto(decimal receitaValue, decimal despesaValue, decimal saldo, List<CategoriaDto> categoriaOverAll)
    {
        ReceitaValue = receitaValue;
        DespesaValue = despesaValue;
        Saldo = saldo;
        CategoriaOverAll = categoriaOverAll;
    }

    public ResumoDto()
    {
    }

    public decimal ReceitaValue { get; set; }
    public decimal DespesaValue { get; set; }
    public decimal Saldo { get; set; }
    public List<CategoriaDto> CategoriaOverAll { get; set; }
}