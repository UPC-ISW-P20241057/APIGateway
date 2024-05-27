using System.ComponentModel.DataAnnotations;

namespace JwtAuthenticationManager.Domain.Services.Communication;

public class RegisterRequest
{
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    [MaxLength(15)]
    public string Role { get; set; }
    [Required]
    [Length(9, 9)]
    public string Phone { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
}