using System.Transactions;
using AutoMapper;
using Films.Domain.Models;
using Films.DTOs;
using Kirel.Repositories.Core.Interfaces;
using Kirel.Repositories.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Films.Core.Services;

/// <summary>
/// Service responsible for film-related operations and logic.
/// </summary>
public class FilmService
{
    private readonly IKirelGenericEntityRepository<int, Film> _filmRepository;
    private readonly IKirelGenericEntityRepository<int, Genre> _genreRepository;
    private readonly IMapper _mapper;


    /// <summary>
    /// Initializes a new instance of the <see cref="FilmService" /> class.
    /// </summary>
    /// <param name="filmRepository"> The repository for accessing film data. </param>
    /// <param name="mapper"> AutoMapper instance </param>
    /// <param name="genreRepository"> The repository to accessing Genre data </param>
    public FilmService(IKirelGenericEntityRepository<int, Film> filmRepository, IMapper mapper,
        IKirelGenericEntityRepository<int, Genre> genreRepository)
    {
        _filmRepository = filmRepository;
        _mapper = mapper;
        _genreRepository = genreRepository;

    }

    /// <summary>
    /// Searches for films in the database based on the provided film name.
    /// </summary>
    /// <param name="filmName"> The name of the film to search for. </param>
    /// <returns> A list of FilmDto objects representing the matching films. </returns>
    /// <exception cref="FilmNotFoundException"> Thrown if no films matching the provided name are found. </exception>
    public async Task<List<FilmDto>> SearchFilms(string filmName)
    {
        // Search for films in the database based on the provided film name.
        var existingFilms = await _filmRepository.GetList(
            m => m.Name != null && m.Name.Contains(filmName),
            includes: q => q.Include(f => f.Genres)
        );
        if (existingFilms == null || !existingFilms.Any())
            throw new FilmNotFoundException($"Film with the title '{filmName}' was not found in the database.");

        // Map the existingFilms to FilmDto objects
        var filmDtos = _mapper.Map<List<FilmDto>>(existingFilms);

        return filmDtos;
    }

    /// <summary>
    /// Creates a new film in the database with the provided data and associated genres.
    /// </summary>
    /// <param name="filmCreateDto"> Data for the new film. </param>
    /// <returns> An asynchronous task representing the operation execution. </returns>
    public async Task CreateFilm(FilmCreateDto filmCreateDto)
    {
        var film = _mapper.Map<Film>(filmCreateDto);

        var existingGenres = await _genreRepository.GetList(orderBy: null, includes: null, page: 0, pageSize: 0);

        if (filmCreateDto.Genres != null)
        {
            var genresToRemove = film.Genres.ToList(); // Создаем список для жанров, которые нужно удалить

            foreach (var genreDto in filmCreateDto.Genres)
            {
                var existingGenre = existingGenres.FirstOrDefault(g => g.Name == genreDto.Name);
                if (existingGenre != null)
                {
                    // Прикрепляем существующий жанр к фильму
                    film.Genres.Add(existingGenre);

                    // Если жанр существует, удаляем его из списка для удаления
                    genresToRemove.Remove(existingGenre);
                }
                else
                {
                    // Создаем новый жанр, если он не существует
                    var newGenre = new Genre { Name = genreDto.Name };
                    film.Genres.Add(newGenre);
                }
            }

            // Удаляем ненужные жанры из фильма
            foreach (var genreToRemove in genresToRemove)
            {
                film.Genres.Remove(genreToRemove);
            }
        }

        // Сохраняем фильм в базе данных
        await _filmRepository.Insert(film);
    }
    
    /// <summary>
    /// Searching all films which have this genre Id
    /// </summary>
    /// <param name="genreIds"></param>
    /// <returns></returns>
    public async Task<List<FilmDto>> GetFilmsByGenreIds(List<int> genreIds)
    {
        var filmDtos = new List<FilmDto>();

        var films = await _filmRepository.GetList(
            m => genreIds.Contains(m.Id), // Поиск по идентификаторам жанров
            includes: q => q.Include(f => f.Genres) // Включение связанных жанров
        );

        foreach (var film in films)
        {
            var filmDto = _mapper.Map<FilmDto>(film);
            filmDtos.Add(filmDto);
        }

        return filmDtos;
    }




/// <summary>
    /// Updates an existing film.
    /// </summary>
    /// <param name="filmId"> The ID of the film to update. </param>
    /// <param name="filmDto"> The DTO containing the updated film information. </param>
    public async Task UpdateFilm(int filmId, FilmUpdateDto filmDto)
    {
        // Retrieve the existing film by its ID.
        var existingFilm = await _filmRepository.GetById(filmId);
        if (existingFilm == null)
            throw new FilmNotFoundException($"Film with ID '{filmId}' was not found in the database.");

        // Update the existing film entity with the new data from the DTO.
        _mapper.Map(filmDto, existingFilm);

        // Perform the update in the repository.
        await _filmRepository.Update(existingFilm);
    }

    /// <summary>
    /// Retrieves a paginated list of all films in the database.
    /// </summary>
    /// <param name="pageNumber"> Page number of the paginated results. </param>
    /// <param name="pageSize"> Number of items per page. </param>
    /// <param name="orderBy"> Field by which the results should be ordered. </param>
    /// <param name="orderDirection"> Sorting direction (ascending or descending). </param>
    /// <param name="search"> Search term to filter the results. </param>
    /// <returns> Paginated result containing a list of FilmDto objects. </returns>
    public async Task<PaginatedResult<List<FilmDto>>> GetAllFilmsPaginated(int pageNumber = 0, int pageSize = 0,
        string orderBy = "", SortDirection orderDirection = SortDirection.Asc, string search = "")
    {
        // Get the total count of films in the database based on the search criteria.
        var totalCount = await _filmRepository.Count(search);

        // Generate pagination information.
        var pagination = Pagination.Generate(pageNumber, pageSize, totalCount);

        // Retrieve a paginated list of films based on the pagination and sorting parameters.
        var films = await _filmRepository.GetList(search, orderBy, orderDirection, pagination.CurrentPage,
            pagination.PageSize);

        // Map the retrieved films to DTOs.
        var filmsDto = _mapper.Map<List<FilmDto>>(films);

        // Create and return the paginated result.
        var result = new PaginatedResult<List<FilmDto>>
        {
            Pagination = pagination,
            Data = filmsDto
        };
        return result;
    }
    


    /// <summary>
    /// Deletes a film from the database.
    /// </summary>
    /// <param name="filmId"> The ID of the film to delete. </param>
    public async Task DeleteFilm(int filmId)
    {
        // Retrieve the film by its ID.
        var film = await _filmRepository.GetById(filmId);

        if (film == null) throw new FilmNotFoundException($"Film with ID '{filmId}' was not found in the database.");

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
        /// <param name="message"> </param>
        public FilmNotFoundException(string message) : base(message)
        {
        }
    }
}