using System.ComponentModel.DataAnnotations;

namespace Challenge.Services.ViewModels;

public class CreateReceitaViewModel
{
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
}