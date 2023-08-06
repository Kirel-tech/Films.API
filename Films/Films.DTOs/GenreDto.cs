namespace Films.DTOs
{
    /// <summary>
    /// Data transfer object (DTO) representing a film genre.
    /// </summary>
    public class GenreDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the genre.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the genre.
        /// </summary>
        public string? Name { get; set; }
    }
}