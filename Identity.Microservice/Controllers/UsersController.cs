using AutoMapper;
using JwtAuthenticationManager.Domain.Models;
using JwtAuthenticationManager.Domain.Services;
using JwtAuthenticationManager.Domain.Services.Communication;
using JwtAuthenticationManager.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Microservice.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController(IUserService userService, IMapper mapper) : ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly IMapper _mapper = mapper;
    
    [HttpPost("sign-in")]
    public async Task<ActionResult<AuthenticationResponse?>> Authenticate([FromBody] AuthenticationRequest authenticationRequest)
    {
        var authenticationResponse = await _userService.Authenticate(authenticationRequest);
        if (authenticationResponse == null) return Unauthorized();
        return authenticationResponse;
    }
    [HttpPost("sign-up")]
    public async Task<IActionResult> RegisterAsync(RegisterRequest request)
    {
        await _userService.RegisterAsync(request);
        return Ok(new {message = "Registration successful"});
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var users = await _userService.ListAsync();
        var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        var resource = _mapper.Map<User, UserResource>(user);
        return Ok(resource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, UpdateRequest request)
    {
        await _userService.UpdateAsync(id, request);
        return Ok(new {message = "User updated successfully."});
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.DeleteAsync(id);
        return Ok(new {message = "User deleted successfully."});
    }
}