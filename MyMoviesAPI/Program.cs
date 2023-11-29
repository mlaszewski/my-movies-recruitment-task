global using MyMoviesAPI.Models;
global using MyMoviesAPI.Data;
using MyMoviesAPI.Services.MovieService;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("VueCorsPolicy", builder =>
    {
        builder
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials()
          .WithOrigins("http://localhost:3000");
    });
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddDbContext<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("VueCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
