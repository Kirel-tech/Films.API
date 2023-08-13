using Films.Domain.Models;
using Kirel.Repositories.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Films.Domain.Models;

/// <summary>
/// Represents a genre associated with films.
/// </summary>
/*[Index(nameof(Name), IsUnique = true)]*/
public class Genre : IKeyEntity<int>, ICreatedAtTrackedEntity
{
    /// <summary>
    /// Gets or sets the name of the genre.
    /// </summary>
    
    public string? Name { get; set; } 

    /// <summary>
    /// reference to film
    /// </summary>
    public List<Film>? Film { get; set; } = new();

    /// <summary>
    /// Gets or sets the Created time of genre
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the genre.
    /// </summary>
    public int Id { get; set; }
    


}