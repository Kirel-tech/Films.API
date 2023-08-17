using AutoMapper;
using Films.Domain.Models;
using Films.DTOs;

namespace Films.Core.Mappers;

/// <summary>
/// Mapping Genre for GenreDTO
/// </summary>
public class GenreMapper : Profile
{
    /// <summary>
    /// GenreMapping constructor
    /// </summary>
    public GenreMapper()
    {
        CreateMap<Genre, GenreDto>().ReverseMap();
        CreateMap<Genre, GenreCreateDto>().ReverseMap();
        CreateMap<Genre, GenreUpdateDto>().ReverseMap();
    }
}