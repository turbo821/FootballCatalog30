using FootballCatalog30.Api.Data;
using FootballCatalog30.Api.Interfaces;
using FootballCatalog30.Api.Repositories;
using FootballCatalog30.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connect = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connect));
builder.Services.AddScoped<IFootballRepository, FootballRepository>();
builder.Services.AddScoped<IFootballService, FootballService>();
builder.Services.AddSignalR();

var app = builder.Build();

app.UseCors("AllowReactApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapHub<PlayersHub>("/playersHub");
app.MapControllers();
app.Run();
