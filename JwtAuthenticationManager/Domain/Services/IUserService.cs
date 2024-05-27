using JwtAuthenticationManager.Domain.Models;
using JwtAuthenticationManager.Domain.Services.Communication;

namespace JwtAuthenticationManager.Domain.Services;

public interface IUserService
{
    Task<IEnumerable<User>> ListAsync();
    Task<User> GetByIdAsync(int id);
    Task<AuthenticationResponse?> Authenticate(AuthenticationRequest authenticationRequest);
    Task RegisterAsync(RegisterRequest request);
    Task UpdateAsync(int id, UpdateRequest request);
    Task DeleteAsync(int id);
}