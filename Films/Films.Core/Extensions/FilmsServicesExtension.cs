using Films.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Films.Core.Extensions;

/// <summary>
/// </summary>
public static class FilmsServicesExtension
{
    /// <summary>
    /// </summary>
    /// <param name="services"> </param>
    public static void AddFilmsServices(this IServiceCollection services)
    {
        services.AddScoped<FilmService>();
    }
}