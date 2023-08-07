using AutoMapper;
using Identity.Core.Services;
using Identity.Domain;
using Identity.DTOs;
using Kirel.Identity.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;

/// <inheritdoc />
[ApiController]
[Authorize]
[Route("authorized/user")]
public class ExAuthorizedUserController : KirelAuthorizedUserController<FilmAuthorizedUserService,Guid,FilmUser,
    FilmAuthorizedUserDto,FilmAuthorizedUserUpdateDto>
{
    /// <inheritdoc />
    public ExAuthorizedUserController(FilmAuthorizedUserService service, IMapper mapper) : base(service, mapper)
    {
    }
}