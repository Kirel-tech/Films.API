using Films.Core;
using Films.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Films.API.Controllers
{
    /// <summary>
    /// Controller for handling film-related operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FilmController : ControllerBase
    {
        private readonly FilmService _filmService; 

        /// <summary>
        /// Initializes a new instance of the <see cref="FilmController"/> class.
        /// </summary>
        /// <param name="filmService">The film service to retrieve and manage film data.</param>
        public FilmController(FilmService filmService)
        {
            _filmService = filmService;
        }

        /// <summary>
        /// Searches for films by name.
        /// </summary>
        /// <param name="filmName">The name of the film to search for.</param>
        /// <returns>Returns a list of films matching the search criteria.</returns>
        [HttpGet("search")]
        public async Task<ActionResult<List<FilmDto>>> SearchFilm(string filmName)
        {
            try
            {
                var films = await _filmService.SearchFilm(filmName);
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
        /// Gets all films.
        /// </summary>
        /// <returns>Returns a list of all films.</returns>
        [HttpGet("all")]
        public async Task<ActionResult<List<FilmDto>>> GetAllFilms()
        {
            try
            {
                var films = await _filmService.GetAllFilms();
                return Ok(films);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Deletes a film by ID.
        /// </summary>
        /// <param name="filmId">The ID of the film to delete.</param>
        /// <returns>Returns a status indicating the success of the delete operation.</returns>
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
        /// <param name="filmDto">The DTO containing the film information to create.</param>
        /// <returns>Returns the ID of the created film.</returns>
        [HttpPost("create")]
        public async Task<ActionResult<int>> CreateFilm(FilmCreateDto filmDto)
        {
            try
            {
                var createdFilmId = await _filmService.CreateFilm(filmDto);
                return Ok(createdFilmId);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Updates a film by ID.
        /// </summary>
        /// <param name="filmId">The ID of the film to update.</param>
        /// <param name="filmDto">The DTO containing the updated film information.</param>
        /// <returns>Returns a status indicating the success of the update operation.</returns>
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
}
