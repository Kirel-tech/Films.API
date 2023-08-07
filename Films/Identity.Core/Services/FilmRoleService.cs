using AutoMapper;
using Identity.Domain;
using Identity.DTOs;
using Kirel.Identity.Core.Services;
using Kirel.Identity.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Identity.Core.Services;

/// <inheritdoc />
public class FilmRoleService : KirelRoleService<Guid, FilmRole, FilmRoleDto, FilmRoleCreateDto,FilmRoleUpdateDto, KirelClaimDto,
    KirelClaimCreateDto, KirelClaimUpdateDto>
{
    /// <inheritdoc />
    public FilmRoleService(RoleManager<FilmRole> roleManager, IMapper mapper) : base(roleManager, mapper)
    {
    }
}