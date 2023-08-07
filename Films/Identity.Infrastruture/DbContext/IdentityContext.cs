using Identity.Domain;
using Kirel.Identity.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastruture.DbContext;

/// <inheritdoc />
public class IdentityContext : KirelIdentityContext<FilmUser, FilmRole, Guid, FilmUserClaim, FilmUserRole, FilmUserLogin,
    FilmRoleClaim, FilmUserToken>
{
    /// <inheritdoc />
    public IdentityContext(DbContextOptions options) : base(options)
    {
    }
}