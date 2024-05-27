using System.ComponentModel.DataAnnotations;

namespace JwtAuthenticationManager.Domain.Services.Communication;

public class UpdateRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    [MaxLength(15)]
    public string Role { get; set; }
    [Length(9, 9)]
    public string Phone { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(100)]
    public string LastName { get; set; }
}