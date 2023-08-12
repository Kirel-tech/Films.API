using Kirel.Repositories.Core.Interfaces;

namespace Films.Domain.Models;

/// <summary>
/// Represents a film entity with details like name, rating, description, and genres.
/// Implements interfaces for creation timestamp tracking and using an integer as the key.
/// </summary>
public class Film : ICreatedAtTrackedEntity, IKeyEntity<int>
{
    /// <summary>
    /// Gets or sets the name of the film.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the rating of the film.
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Gets or sets the description of the film.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the list of genres associated with the film.
    /// </summary>
    public List<Genre> Genres { get; set; } = new();

    /// <summary>
    /// Gets or sets the URL of the film's poster.
    /// </summary>
    public string? PosterUrl { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the film was created.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the film.
    /// </summary>
    public int Id { get; set; }
}