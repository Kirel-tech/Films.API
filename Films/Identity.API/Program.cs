using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Identity.Core.Services;
using Identity.Domain;
using Identity.Infrastruture.DbContext;
using Kirel.Identity.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentity<FilmUser, FilmRole>().AddEntityFrameworkStores<IdentityContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
    // User settings.
    options.User.RequireUniqueEmail = true;
});

// Add dto validators. Configured in Validators.
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

//Add AutoMapper. Class <--> Dto mappings. Configured in Mappings.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Add kirel based identity management services
builder.Services.AddScoped<FilmAuthenticationService>();
builder.Services.AddScoped<FilmAuthorizedUserService>();
builder.Services.AddScoped<FilmRegistrationService>();
builder.Services.AddScoped<FilmRoleService>();
builder.Services.AddScoped<FilmUserService>();

// add kirel based jwt tokens generation service
builder.Services.AddScoped<FilmJwtTokenService>();

// Add kirel authentication/authorization options
var authOptions = new KirelAuthOptions()
{
    AccessLifetime = 5,
    Audience = "ExampleClient",
    Issuer = "ExampleServer",
    Key = "SomeSuperSecretKey123",
    RefreshLifetime = 3600
};
builder.Services.AddSingleton(authOptions);

// Add ASP.NET authentication configuration
builder.Services.AddAuthentication(option =>
    {
        // Fixing 404 error when adding an attribute Authorize to controller
        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = authOptions.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = authOptions.GetSymmetricSecurityKey(authOptions.Key),
            ValidateIssuerSigningKey = true,
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();