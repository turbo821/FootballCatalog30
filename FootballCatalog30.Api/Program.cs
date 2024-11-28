using FootballCatalog30.Api.Data;
using FootballCatalog30.Api.Interfaces;
using FootballCatalog30.Api.Repositories;
using FootballCatalog30.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connect = builder.Configuration.GetConnectionString("DefaultConnection")!;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connect));
builder.Services.AddScoped<IFootballRepository, FootballRepository>();
builder.Services.AddScoped<IFootballService, FootballService>();

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
