using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Films.Infrastructure;

/// <summary>
/// Utility class for initializing the film database and creating tables.
/// </summary>
public class FilmDbInitialize : DbContext
{
    /// <summary>
    /// Initializes the film database and creates tables if they do not exist.
    /// </summary>
    /// <param name="serviceProvider"> The service provider to retrieve the database context. </param>
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<FilmDbContext>();
        context.Database.EnsureCreated();
    }
}