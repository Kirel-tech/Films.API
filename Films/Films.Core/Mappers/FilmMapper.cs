using AutoMapper;
using Films.Domain.Models;
using Films.DTOs;

namespace Films.Core.Mappers;

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