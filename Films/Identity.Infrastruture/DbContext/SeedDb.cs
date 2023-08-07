using Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastruture.DbContext;

/// <summary>
/// Static class for database initialization
/// </summary>
public static class SeedDb
{
    private static RoleManager<FilmRole> _roleManager;
    private static UserManager<FilmUser>_userManager;
    private static async Task<FilmRole> FindOrCreateRole(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role != null) return role;

        role = new FilmRole() { Name = roleName };
        var result = await _roleManager.CreateAsync(role);
        return !result.Succeeded ? null : role;
    }
    private static async Task<FilmUser> FindOrCreateUser(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user != null) return user;
        
        user = new FilmUser()
        {
            UserName = username, Email = $"{username}@kirel.com", Name = username, LastName = "Default"
        };
        var result = await _userManager.CreateAsync(user, $"{username}@123");
        return !result.Succeeded ? null : user;
    }

    private static async Task AddUserToRoleIfNotAlreadyInRole(FilmUser user, string roleName)
    {
        if(!await _userManager.IsInRoleAsync(user, roleName))
            await _userManager.AddToRoleAsync(user, roleName);
    }

    /// <summary>
    /// Database initialization
    /// </summary>
    /// <param name="serviceProvider">Service provider</param>
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<IdentityContext>();
        context.Database.EnsureCreated();
        
        _roleManager = serviceProvider.GetRequiredService<RoleManager<FilmRole>>();
        _userManager = serviceProvider.GetRequiredService<UserManager<FilmUser>>();
        
        var adminUser = await FindOrCreateUser("Admin");
        var adminRole = await FindOrCreateRole("Admin");
        var userRole = await FindOrCreateRole("User");
        var userUser = await FindOrCreateUser("User");
        
        if (adminRole != null && adminUser != null)
        {
            await AddUserToRoleIfNotAlreadyInRole(adminUser, "Admin");
        }

        if (userRole != null && adminUser != null)
        {
            await AddUserToRoleIfNotAlreadyInRole(adminUser, "User");
        }

        if (userRole != null && userUser != null)
        {
            await AddUserToRoleIfNotAlreadyInRole(userUser, "User");
        }
    }
}