using AutoMapper;
using Identity.Domain;
using Identity.DTOs;
using Kirel.Identity.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Identity.Core.Services;

/// <inheritdoc />
public class FilmAuthorizedUserService : KirelAuthorizedUserService<Guid, FilmUser, FilmAuthorizedUserDto, FilmAuthorizedUserUpdateDto>
{
    /// <inheritdoc />
    public FilmAuthorizedUserService(IHttpContextAccessor httpContextAccessor, UserManager<FilmUser> userManager, IMapper mapper) : base(httpContextAccessor, userManager, mapper)
    {
    }
}