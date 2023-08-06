using AutoMapper;
using Films.Domain;                 
using Films.DTOs;                   
using Kirel.Repositories.Interfaces;
using Kirel.Repositories.Sorts;

namespace Films.Core
{
    /// <summary>
    /// Service responsible for film-related operations and logic.
    /// </summary>
    public class FilmService
    {
        private readonly IKirelGenericEntityRepository<int, Film> _filmRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilmService"/> class.
        /// </summary>
        /// <param name="filmRepository">The repository for accessing film data.</param>
        /// <param name="mapper">AutoMapper instance</param>
        public FilmService(IKirelGenericEntityRepository<int, Film> filmRepository, IMapper mapper)
        {
            this._filmRepository = filmRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Searches for films in the database by name.
        /// </summary>
        /// <param name="filmName">The name of the film to search for.</param>
        /// <returns>A list of film DTOs matching the search criteria.</returns>
        public async Task<List<FilmDto>> SearchFilm(string filmName)
        {
            // Search for films in the database based on the provided film name.
            var existingFilm = await _filmRepository.GetList(m => m.Name != null && m.Name.Contains(filmName));

            var enumerable = existingFilm.ToList();
            if (existingFilm == null || !enumerable.Any())
            {
                throw new FilmNotFoundException($"Film with the title '{filmName}' was not found in the database.");
            }

            // Map the retrieved films to DTOs for presentation.
            var filmsList = enumerable.Select(film => new FilmDto
            {
                Id = film.Id,
                Name = film.Name,
                Rating = film.Rating,
                Description = film.Description,
                PosterURL = film.PosterUrl,
                Genres = film.Genres
            }).ToList();

            return filmsList;
        }
       /// <summary>
        /// Creates a new film.
        /// </summary>
        /// <param name="filmDto">The DTO containing the film information to create.</param>
        /// <returns>The ID of the created film.</returns>
        public async Task<int> CreateFilm(FilmCreateDto filmDto)
        {
            // Map the DTO to an entity and insert it into the repository.
            var film = _mapper.Map<Film>(filmDto);
            var createdFilm = await _filmRepository.Insert(film);
            return createdFilm.Id;
        }

        /// <summary>
        /// Updates an existing film.
        /// </summary>
        /// <param name="filmId">The ID of the film to update.</param>
        /// <param name="filmDto">The DTO containing the updated film information.</param>
        public async Task UpdateFilm(int filmId, FilmUpdateDto filmDto)
        {
            // Retrieve the existing film by its ID.
            var existingFilm = await _filmRepository.GetById(filmId);
            if (existingFilm == null)
            {
                throw new FilmNotFoundException($"Film with ID '{filmId}' was not found in the database.");
            }

            // Update the existing film entity with the new data from the DTO.
            _mapper.Map(filmDto, existingFilm);

            // Perform the update in the repository.
            await _filmRepository.Update(existingFilm);
        }
        
        /// <summary>
        /// Gets a list of all films.
        /// </summary>
        /// <returns>A list of film DTOs representing all films in the database.</returns>
        public async Task<IEnumerable<FilmDto>> GetAllFilms()
        {
            // Get the total count of films in the database.
            var allCount = await _filmRepository.Count(expression: null);

            // Retrieve a paginated list of all films and map them to DTOs.
            var films = await _filmRepository.GetList(null, "Created", SortDirection.Asc, 1, allCount);
            var filmsDto = _mapper.Map<List<FilmDto>>(films);
            return filmsDto;
        }

        /// <summary>
        /// Deletes a film from the database.
        /// </summary>
        /// <param name="filmId">The ID of the film to delete.</param>
        public async Task DeleteFilm(int filmId)
        {
            // Retrieve the film by its ID.
            var film = await _filmRepository.GetById(filmId);

            if (film == null)
            {
                throw new FilmNotFoundException($"Film with ID '{filmId}' was not found in the database.");
            }

            // Delete the film from the repository.
            await _filmRepository.Delete(filmId);
        }

        /// <summary>
        /// Custom exception class for indicating that a film was not found.
        /// </summary>
        public class FilmNotFoundException : Exception
        {
            /// <summary>
            /// Retrieves exception message 
            /// </summary>
            /// <param name="message"></param>
            public FilmNotFoundException(string message) : base(message)
            {
            }
        }
    }
}
