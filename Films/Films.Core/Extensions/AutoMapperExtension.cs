using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Films.Core.Extensions;

/// <summary>
/// </summary>
public static class AutoMapperExtension
{
    /// <summary>
    /// </summary>
    /// <param name="services"> </param>
    public static void AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}