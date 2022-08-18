using System.ComponentModel.DataAnnotations;

namespace Challenge.Services.ViewModels;

public class UpdateDespesaViewModel
{
    [Required(ErrorMessage = "O ID não pode ser nulo.")]
    [Range(1, long.MaxValue, ErrorMessage = "O ID não pode ser menor que 1")]
    public long Id { get; set; }
    [Required(ErrorMessage = "A Descricao da Despesa não pode ser nula.")]
    [MinLength(3, ErrorMessage = "A descrição deve ter no mínimo 3 caracteres")]
    [MaxLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres")]
    public string Descricao { get; set; }
    [Required(ErrorMessage = "O Valor da Despesa não pode ser nulo.")]
    [Range(1, 1000000000, ErrorMessage = "O valor deve ter no mínimo 1 e no máximo 1000000000")]
    public double Valor { get; set; }
    [Required(ErrorMessage = "A Data da Despesa não pode ser nula.")]
    [Range(typeof(DateTime), "2021/12/31", "2023/01/01")]
    public DateTime Data { get; set; }
    [Required(AllowEmptyStrings = true)]
    public string Categorias { get; set; }
}