namespace Challenge.Security.ViewModels;

public class AuthToken
{
    public string Message { get; set; }
    public string Token { get; set; }
    public string TokenType { get; set; } = "Bearer";
    public long ExpiresIn { get; set; }
}