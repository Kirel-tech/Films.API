using Identity.Core.Services;
using Identity.Domain;
using Identity.DTOs;
using Kirel.Identity.Controllers;
using Kirel.Identity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;

/// <inheritdoc />
[ApiController]
[Authorize]
[Route("users")]
public class ExUsersController : KirelUsersController<FilmUserService, Guid, FilmUser, FilmRole, FilmUserDto, FilmUserCreateDto,
    FilmUserUpdateDto, KirelClaimDto, KirelClaimCreateDto, KirelClaimUpdateDto>
{
    /// <inheritdoc />
    public ExUsersController(FilmUserService service) : base(service)
    {
    }
}