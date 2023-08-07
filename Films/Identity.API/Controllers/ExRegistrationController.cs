using Identity.Core.Services;
using Identity.Domain;
using Identity.DTOs;
using Kirel.Identity.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;

/// <inheritdoc />
[ApiController]
[Route("registration")]
public class ExRegistrationController : KirelRegistrationController<FilmRegistrationService, FilmUserRegistrationDto, Guid, FilmUser>
{
    /// <inheritdoc />
    public ExRegistrationController(FilmRegistrationService service) : base(service)
    {
    }
}