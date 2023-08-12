using Films.Domain.Models;
using Kirel.Repositories.Core.Interfaces;
using Kirel.Repositories.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

namespace Films.Infrastructure.Extentions;

/// <summary>
/// Add db to DI
/// </summary>
public static class FilmsRepositoriesExtension
{
    /// <summary>
    /// </summary>
    /// <param name="services"> Collection services </param>
    public static void AddFilmsRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IKirelGenericEntityRepository<int, Genre>,
                KirelGenericEntityFrameworkRepository<int, Genre, FilmDbContext>>();
        services
            .AddScoped<IKirelGenericEntityRepository<int, Film>,
                KirelGenericEntityFrameworkRepository<int, Film, FilmDbContext>>();
    }
}