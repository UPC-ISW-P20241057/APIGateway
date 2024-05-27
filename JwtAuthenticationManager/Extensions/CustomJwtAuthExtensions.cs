using System.Text;
using JwtAuthenticationManager.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthenticationManager.Extensions;

public static class CustomJwtAuthExtensions
{
    public static void AddCustomJwtAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = false;
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(UserService.JWT_SECURITY_KEY))
            };
        });
    }
}