using AutoMapper;
using Identity.Domain;
using Identity.DTOs;
using Kirel.Identity.Core.Services;
using Microsoft.AspNetCore.Identity;

namespace Identity.Core.Services;

/// <inheritdoc />
public class FilmRegistrationService : KirelRegistrationService<Guid, FilmUser, FilmUserRegistrationDto>
{
    /// <inheritdoc />
    public FilmRegistrationService(UserManager<FilmUser> userManager, IMapper mapper) : base(userManager, mapper)
    {
    }
}