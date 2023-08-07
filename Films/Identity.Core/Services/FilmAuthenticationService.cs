
using Identity.Domain;
using Kirel.Identity.Core.Services;
using Microsoft.AspNetCore.Identity;

namespace Identity.Core.Services;

/// <inheritdoc />
public class FilmAuthenticationService : KirelAuthenticationService<Guid, FilmUser>
{
    /// <inheritdoc />
    public FilmAuthenticationService(UserManager<FilmUser> userManager) : base(userManager)
    {
    }
}