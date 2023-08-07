using Films.Domain;                  
using Microsoft.EntityFrameworkCore;  

namespace Films.Infrastructure
{
    /// <summary>
    /// Represents the database context for managing film-related data.
    /// </summary>
    public class FilmDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilmDbContext"/> class.
        /// </summary>
        /// <param name="options">The options used to configure the context.</param>
        public FilmDbContext(DbContextOptions<FilmDbContext> options) : base(options)
        {
            
        }
        /// <summary>
        /// Gets or sets the DbSet for films in the database.
        /// </summary>
        private DbSet<Film>? Films { get; set; }
        
        /// <summary>
        /// Gets or sets the DbSet for genres in the database.
        /// </summary>
        private DbSet<Genre>? Genres { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Film>()
                .HasMany(f => f.Genres)
                .WithMany() 
                .UsingEntity(j =>
                {
                    
                    j.Property<int>("Id");
                });
        }
        
        
    }
}