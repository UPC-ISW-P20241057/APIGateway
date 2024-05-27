namespace JwtAuthenticationManager.Domain.Services.Communication;

public class AuthenticationResponse
{
    public long Id { get; set; }
    public string Email { get; set; }
    public char Role { get; set; }
    public string Phone { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string JwtToken { get; set; }
    public int ExpiresIn { get; set; }
}