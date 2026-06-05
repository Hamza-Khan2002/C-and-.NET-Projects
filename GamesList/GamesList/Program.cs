using GamesList.Data;
using GamesList.Mapping;
using GamesList.Services.GameService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connstring = builder.Configuration.GetConnectionString("AddDb");

builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connstring));

builder.Services.AddScoped<IGameServices, GameService>();

builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(GameMapping)));

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
