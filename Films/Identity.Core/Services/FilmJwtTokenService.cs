using Identity.Domain;
using Kirel.Identity.Core.Models;
using Kirel.Identity.Jwt.Core.Services;
using Microsoft.AspNetCore.Identity;

namespace Identity.Core.Services;

/// <inheritdoc />
public class FilmJwtTokenService : KirelJwtTokenService<Guid, FilmUser, FilmRole>
{
    /// <inheritdoc />
    public FilmJwtTokenService(UserManager<FilmUser> userManager, RoleManager<FilmRole> roleManager, KirelAuthOptions authOptions) : base(userManager, roleManager, authOptions)
    {
    }
}