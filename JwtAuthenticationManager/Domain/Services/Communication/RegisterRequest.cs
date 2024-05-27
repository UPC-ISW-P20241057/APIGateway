﻿using System.ComponentModel.DataAnnotations;

namespace JwtAuthenticationManager.Domain.Services.Communication;

public class RegisterRequest
{
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    [StringLength(1)]
    public char Role { get; set; }
    [Required]
    [StringLength(9)]
    public string Phone { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
}