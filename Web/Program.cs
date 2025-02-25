using System.Reflection;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile($"appsettings.Development.json", true, true);
// Add services to the container.
var presentationAssembly = Assembly.Load("Presentation");
builder.Services.AddControllers().AddApplicationPart(presentationAssembly);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<OrderseerContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));
Console.WriteLine(builder.Configuration.GetConnectionString("Postgres"));
Console.WriteLine(builder.Environment.IsDevelopment());
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
Console.WriteLine();
app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();