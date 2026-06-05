using FinanceProject.Data;
using FinanceProject.DTO.Stock;
using FinanceProject.Interfaces;
using FinanceProject.Mapper;
using FinanceProject.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure Controllers
builder.Services.AddControllers();

//Add Swagger UI
builder.Services.AddSwaggerGen();

//Configure Databse
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Configure Repository using Dependency Injection
builder.Services.AddScoped<IStockRepository, StockRepository>();

//Configure Mapping
builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(StockDto)));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
