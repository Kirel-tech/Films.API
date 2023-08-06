using System.Reflection;
using Films.Core;
using Films.Domain;
using Films.Infrastructure;
using Kirel.Repositories;
using Kirel.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;                            

var builder = WebApplication.CreateBuilder(args);

/*var authOptions = builder.Configuration.GetSection("AuthOptions").Get<AuthOptions>();*/

//taking connection string from appsettings.json 
var connectionString = builder.Configuration.GetConnectionString("PostgreConnection");
builder.Services.AddDbContext<FilmDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add services to the container.
builder.Services.AddScoped<FilmService>();
builder.Services.AddScoped<IKirelGenericEntityRepository<int, Film>, KirelGenericEntityFrameworkRepository<int, Film, FilmDbContext>>();
 
//Add AutoMapper 

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
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

app.UseAuthorization();

app.MapControllers();

app.Run();