using Kirel.Repositories.Interfaces;

namespace Films.Domain
{
    /// <summary>
    /// Represents a genre associated with films.
    /// </summary>
    public class Genre : IKeyEntity<int>, ICreatedAtTrackedEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the genre.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name of the genre.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Created { get; set; }
    }
}