using AutoMapper;
using Identity.Domain;
using Identity.DTOs;

namespace Identity.Core;

/// <summary>
/// Mapping profile for user
/// </summary>
public class ExUserMapping : Profile
{
    /// <summary>
    /// UserMapping constructor
    /// </summary>
    public ExUserMapping()
    {
        CreateMap<FilmUser, FilmUserDto>().ReverseMap();
        CreateMap<FilmUser, FilmUserCreateDto>().ReverseMap();
        CreateMap<FilmUser, FilmUserUpdateDto>().ReverseMap();
        CreateMap<FilmUser, FilmUserRegistrationDto>().ReverseMap();
        CreateMap<FilmUser, FilmAuthorizedUserDto>().ReverseMap();
        CreateMap<FilmUser, FilmAuthorizedUserUpdateDto>().ReverseMap();
    }
}