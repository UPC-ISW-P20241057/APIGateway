using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using JwtAuthenticationManager.Domain.Models;
using JwtAuthenticationManager.Domain.Repositories;
using JwtAuthenticationManager.Domain.Services;
using JwtAuthenticationManager.Domain.Services.Communication;
using JwtAuthenticationManager.Exceptions;
using Microsoft.IdentityModel.Tokens;
using BCryptNet = BCrypt.Net.BCrypt;

namespace JwtAuthenticationManager.Services;

public class UserService(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork): IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public const string JWT_SECURITY_KEY = "ghrthgfhgfderftewtregdffeswarfewtfdsfdsfg";
    private const int JWT_TOKEN_VALIDITY_MINS = 20;

    public async Task<IEnumerable<User>> ListAsync()
    {
        return await  _userRepository.ListAsync();
    }

    public async Task<User> GetByIdAsync(long id)
    {
        var user = await _userRepository.FindByIdAsync(id);
        if (user == null)
            throw new KeyNotFoundException("User not found.");
        return user;
    }
    
    public async Task<AuthenticationResponse?> Authenticate(AuthenticationRequest authenticationRequest)
    {
        if (string.IsNullOrWhiteSpace(authenticationRequest.Email) || string.IsNullOrWhiteSpace(authenticationRequest.Password))
            return null;

        var user = await _userRepository.FindByEmailAsync(authenticationRequest.Email);
        
        if (user == null || !BCryptNet.Verify(authenticationRequest.Password, user.PasswordHash)) return null;

        var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
        var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
        var claimsIdentity = new ClaimsIdentity(new List<Claim>
        {
            new(JwtRegisteredClaimNames.Name, authenticationRequest.Email),
            new("Role", user.Role)
        });
        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);
        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = tokenExpiryTimeStamp,
            SigningCredentials = signingCredentials
        };
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        var token = jwtSecurityTokenHandler.WriteToken(securityToken);

        var response = _mapper.Map<User, AuthenticationResponse>(user);
        response.JwtToken = token;
        response.ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds;
        return response;
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        if (_userRepository.ExistsByEmail(request.Email))
            throw new AppException($"Email '{request.Email}' is already taken.");

        var user = _mapper.Map<User>(request);

        user.PasswordHash = BCryptNet.HashPassword(request.Password);

        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while saving the user: {e.Message}");
        }
    }
    
    public async Task UpdateAsync(long id, UpdateRequest request)
    {

        var user = GetById(id);
        var userWithEmail = await _userRepository.FindByEmailAsync(request.Email);
        
        if(userWithEmail != null && userWithEmail.Id != user.Id)
            throw new AppException($"Email '{request.Email}' is already taken.");

        if (!string.IsNullOrEmpty(request.Password))
            user.PasswordHash = BCryptNet.HashPassword(request.Password);

        _mapper.Map(request, user);
        try
        {
            _userRepository.Update(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while updating the user: {e.Message}");
        }
    }

    public async Task DeleteAsync(long id)
    {
        var user = GetById(id);
        try
        {
            _userRepository.Remove(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while deleting the user: {e.Message}");
        }
    }

    private User GetById(long id)
    {
        var user = _userRepository.FindById(id);
        if (user == null) throw new KeyNotFoundException("User not found.");
        return user;
    }
}