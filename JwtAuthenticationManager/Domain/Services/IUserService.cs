using JwtAuthenticationManager.Domain.Models;
using JwtAuthenticationManager.Domain.Services.Communication;

namespace JwtAuthenticationManager.Domain.Services;

public interface IUserService
{
    Task<IEnumerable<User>> ListAsync();
    Task<User> GetByIdAsync(long id);
    Task<AuthenticationResponse?> Authenticate(AuthenticationRequest authenticationRequest);
    Task<(bool, string)> RegisterAsync(RegisterRequest request);
    Task UpdateAsync(long id, UpdateRequest request);
    Task DeleteAsync(long id);
}