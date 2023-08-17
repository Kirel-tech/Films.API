using System.Reflection;
using Authentication.Shared;
using Films.API.Extensions;
using Films.Core.Extensions;
using Films.Infrastructure;
using Films.Infrastructure.Extentions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var jwtOptions = builder.Configuration.GetSection("JwtAuthenticationOptions").Get<JwtAuthenticationOptions>();

//taking connection string from appsettings.json 
var connectionString = builder.Configuration.GetConnectionString("SqlConnection");
builder.Services.AddDbContext<FilmDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddFilmsServices();
builder.Services.AddFilmsRepositories();


//Add AutoMapper. Class <--> Dto mappings. Configured in Mappings.
builder.Services.AddMapper();

/*builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());*/
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
//Add authentication 
builder.Services.AddAuthentication();
builder.Services.AddAuthenticationConfigurations(jwtOptions);

// Configure swagger
builder.Services.AddSwagger();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
FilmDbInitialize.Initialize(app.Services.GetRequiredService<IServiceProvider>().CreateScope().ServiceProvider);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();