namespace JwtAuthenticationManager.Domain.Services.Communication;

public class AuthenticationRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}