using AutoMapper;
using JwtAuthenticationManager.Domain.Models;
using JwtAuthenticationManager.Domain.Services.Communication;
using JwtAuthenticationManager.Resources;

namespace JwtAuthenticationManager.Mapping;

public class ModelToResourceProfile: Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<User, AuthenticationResponse>();
        CreateMap<User, UserResource>();
    }
}