using Authentication.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Films.API.Extensions;

/// <summary>
/// Authentication configuration extension
/// </summary>
public static class AuthenticationExtension
{
    /// <summary>
    /// Add authentication configuration to DI
    /// </summary>
    /// <param name="services"> services collection </param>
    /// <param name="authOptions"> JWT Token generation config </param>
    public static void AddAuthenticationConfigurations(this IServiceCollection services,
        JwtAuthenticationOptions authOptions)
    {
        services.AddAuthentication(option =>
            {
                // Fixing 404 error when adding an attribute Authorize to controller
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = authOptions.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = authOptions.GetSymmetricSecurityKey(authOptions.Key),
                    ValidateIssuerSigningKey = true
                };
            });
    }
}