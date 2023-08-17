using Films.Core.Services;
using Films.Domain.Models;
using Films.DTOs;
using Kirel.Repositories.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Films.API.Controllers;

/// <summary>
/// Controller for handling film-related operations.
/// </summary>
[ApiController]
[Route("api/films")]
public class FilmController : ControllerBase
{
    private readonly FilmService _filmService;

    /// <summary>
    /// Initializes a new instance of the <see cref="FilmController" /> class.
    /// </summary>
    /// <param name="filmService"> The film service to retrieve and manage film data. </param>
    public FilmController(FilmService filmService)
    {
        _filmService = filmService;
    }

    /// <summary>
    /// Searches for films by name.
    /// </summary>
    /// <param name="filmName"> The name of the film to search for. </param>
    /// <returns> Returns a list of films matching the search criteria. </returns>
    [HttpGet("search")]
    public async Task<ActionResult<List<FilmDto>>> SearchFilms(string filmName)
    {
        try
        {
            var films = await _filmService.SearchFilms(filmName);
            return Ok(films);
        }
        catch (FilmService.FilmNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            // If an unexpected exception occurs return a 500 Internal Server Error response.
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Retrieves a paginated list of films based on the specified parameters.
    /// </summary>
    /// <param name="pageNumber"> Page number of the paginated results. </param>
    /// <param name="pageSize"> Number of items per page. </param>
    /// <param name="orderBy"> Field by which the results should be ordered. </param>
    /// <param name="orderDirection"> Sorting direction (ascending or descending). </param>
    /// <param name="search"> Search term to filter the results. </param>
    /// <param name="genreIds"> </param>
    /// <returns> Paginated result containing a list of FilmDto objects. </returns>
    [HttpGet("all")]
    public async Task<ActionResult<PaginatedResult<List<FilmDto>>>> GetAllFilms(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? orderBy = "",
        [FromQuery] string orderDirection = "asc",
        [FromQuery] string? search = "",
        [FromQuery] List<int>? genreIds = null)
    {
        
            SortDirection directionEnum;
            if (orderDirection == "asc")
            {
                directionEnum = SortDirection.Asc;
            }
            else
            {
                directionEnum = SortDirection.Desc;
            }

            var result = await _filmService.GetAllFilmsPaginated(
                pageNumber, pageSize, orderBy!, directionEnum, search!, genreIds);

            return Ok(result);
        
    }


    /// <summary> Deletes a film by ID. </summary>
    /// <param name="filmId"> The ID of the film to delete. </param>
    /// <returns> Returns a status indicating the success of the delete operation. </returns>
    [HttpDelete("{filmId}")]
    public async Task<IActionResult> DeleteFilm(int filmId)
    {
        try
        {
            await _filmService.DeleteFilm(filmId);
            return Ok();
        }
        catch (FilmService.FilmNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Creates a new film.
    /// </summary>
    /// <param name="filmCreateDto"> The DTO containing the film information to create. </param>
    /// <returns> Returns the ID of the created film. </returns>
    [HttpPost]
    public async Task<IActionResult> CreateFilm([FromBody] FilmCreateDto filmCreateDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _filmService.CreateFilm(filmCreateDto);

        return Ok();
    }
    /// <summary>
    /// Get all films by genres
    /// </summary>
    /// <param name="genreIds">Ids of genres you need </param>
    /// <returns></returns>
    [HttpGet("films/by-genre-ids")]
    public async Task<IActionResult> GetFilmsByGenreIds([FromQuery] List<int> genreIds)
    {
        var filmDtos = await _filmService.GetFilmsByGenreIds(genreIds);
        return Ok(filmDtos);
    }
    /// <summary>
    /// Updates a film by ID.
    /// </summary>
    /// <param name="filmId"> The ID of the film to update. </param>
    /// <param name="filmDto"> The DTO containing the updated film information. </param>
    /// <returns> Returns a status indicating the success of the update operation. </returns>
    [HttpPut("update/{filmId}")]
    public async Task<IActionResult> UpdateFilm(int filmId, FilmUpdateDto filmDto)
    {
        try
        {
            await _filmService.UpdateFilm(filmId, filmDto);
            return Ok();
        }
        catch (FilmService.FilmNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}