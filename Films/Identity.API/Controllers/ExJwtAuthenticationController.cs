using Identity.Core.Services;
using Identity.Domain;
using Identity.DTOs;
using Kirel.Identity.Jwt.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;

/// <inheritdoc />
[ApiController]
[Route("authentication/jwt")]
public class ExJwtAuthenticationController : KirelJwtAuthenticationController<FilmJwtTokenService,FilmAuthenticationService,
    FilmAuthorizedUserService, Guid, FilmUser, FilmRole, FilmAuthorizedUserDto, FilmAuthorizedUserUpdateDto>
{
    /// <inheritdoc />
    public ExJwtAuthenticationController(FilmAuthenticationService authService, FilmJwtTokenService tokenService, FilmAuthorizedUserService authorizedUserservice) : base(authService, tokenService, authorizedUserservice)
    {
    }
}