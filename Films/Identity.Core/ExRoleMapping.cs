using AutoMapper;
using Identity.Domain;
using Identity.DTOs;

namespace Identity.Core;

/// <summary>
/// Mapping profile for role
/// </summary>
public class ExRoleMapping : Profile
{
    /// <summary>
    /// Role mapping constructor
    /// </summary>
    public ExRoleMapping()
    {
        CreateMap<FilmRole, FilmRoleDto>().ReverseMap();
        CreateMap<FilmRole, FilmRoleCreateDto>().ReverseMap();
        CreateMap<FilmRole, FilmRoleUpdateDto>().ReverseMap();
    }
}