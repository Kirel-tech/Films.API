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
[Route("roles")]
public class ExRolesController : KirelRolesController<FilmRoleService, Guid, FilmRole, FilmRoleDto, FilmRoleCreateDto,
    FilmRoleUpdateDto,KirelClaimDto,KirelClaimCreateDto,KirelClaimUpdateDto>
{
    /// <inheritdoc />
    public ExRolesController(FilmRoleService service) : base(service)
    {
    }
}