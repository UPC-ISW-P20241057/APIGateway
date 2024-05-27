using AutoMapper;
using JwtAuthenticationManager.Domain.Models;
using JwtAuthenticationManager.Domain.Services.Communication;

namespace JwtAuthenticationManager.Mapping;

public class ResourceToModelProfile: Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<RegisterRequest, User>();
        CreateMap<UpdateRequest, User>()
            .ForAllMembers(options => options.Condition(
                (source, Target, property) =>
                {
                    if (property == null) return false;
                    if (property.GetType() == typeof(string) && 
                        string.IsNullOrEmpty((string) property)) return false;
                    return true;
                }));
    }
}