using AutoMapper;
using Films.Domain;
using Films.DTOs;

namespace Films.Core;
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
    }
}