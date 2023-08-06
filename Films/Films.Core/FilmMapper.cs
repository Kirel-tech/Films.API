using Films.Domain;
using Films.DTOs;
using AutoMapper;
namespace Films.Core;

/// <summary>
/// Mapping Film for FilmDTO
/// </summary>
public class FilmMapper : Profile
{
    /// <summary>
    /// Films constructor
    /// </summary>
    public FilmMapper()
    {
        CreateMap<Film, FilmDto>().ReverseMap();
        CreateMap<Film, FilmCreateDto>().ReverseMap();
        CreateMap<Film, FilmUpdateDto>().ReverseMap();
    }
}