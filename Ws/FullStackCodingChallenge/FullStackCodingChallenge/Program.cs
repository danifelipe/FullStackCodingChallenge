using Microsoft.Extensions.DependencyInjection;
using Infrastructure;
using Services;
using Domain.Interface;
using Infrastructure.Repositories;
using Domain.Models;



var builder = WebApplication.CreateBuilder(args);




var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registrar DatabaseContext correctamente
builder.Services.AddSingleton<DatabaseContext>();

// Registrar los repositorios genéricos
builder.Services.AddScoped<Domain.Interface.IRepository<Item>, ItemRepository>(); // 💡 Corrección aquí
builder.Services.AddScoped<ItemService>();




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.Urls.Add("http://0.0.0.0:8080");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
