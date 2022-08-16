using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Challenge.Security.ViewModels;

public class RegisterViewModel
{
    [Required]
    [MaxLength(20, ErrorMessage = "Username has max length of 20 characters")]
    public string UserName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [DataType(DataType.Password)]
    [DisplayName("Confirm password")]
    [Compare("Password", ErrorMessage = "Password and confirmation password not match")]
    public string ConfirmPassword { get; set; }
}