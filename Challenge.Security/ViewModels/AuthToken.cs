namespace Challenge.Security.ViewModels;

public class AuthToken
{
    public string Token { get; set; }
    public string TokenType { get; set; } = "Bearer";
}