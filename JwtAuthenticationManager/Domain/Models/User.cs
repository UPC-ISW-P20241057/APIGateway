﻿namespace JwtAuthenticationManager.Domain.Models;

public class User
{
    public long Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }
    public string Phone { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
}