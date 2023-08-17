namespace Films.DTOs;

/// <summary>
/// Film UpdateDto
/// </summary>
public class FilmUpdateDto
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
    public List<GenreUpdateDto>? Genres { get; set; }

    /// <summary>
    /// Gets or sets the URL of the film's poster.
    /// </summary>
    public string? PosterUrl { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the film was created.
    /// </summary>
    public DateTime Created { get; set; }
}