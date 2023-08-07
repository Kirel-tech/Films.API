using AutoMapper;
using Identity.Domain;
using Identity.DTOs;
using Kirel.Identity.Core.Services;
using Kirel.Identity.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Identity.Core.Services;
/// <inheritdoc />
public class FilmUserService : KirelUserService<Guid, FilmUser, FilmRole, FilmUserDto, FilmUserCreateDto, FilmUserUpdateDto, KirelClaimDto,
    KirelClaimCreateDto,KirelClaimUpdateDto>
{
    /// <inheritdoc />
    public FilmUserService(UserManager<FilmUser> userManager, RoleManager<FilmRole> roleManager, IMapper mapper) : base(userManager, roleManager, mapper)
    {
    }
}