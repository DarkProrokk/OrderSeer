using System.Reflection;
using Application.Handlers;
using Application.Interfaces;
using Application.Services;
using Confluent.Kafka;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure;
using Infrastructure.Kafka;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile($"appsettings.Development.json", true, true);


// Add services to the container.
var presentationAssembly = Assembly.Load("Presentation");
builder.Services.AddControllers().AddApplicationPart(presentationAssembly);
// Learn more about configuring OpenAPI at https://aka.ms/asp net/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<OrderseerContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

#region KafkaServices
var kafkaConfig = builder.Configuration.GetSection("Kafka").GetSection("Consumer");
var consumerConfig = kafkaConfig.Get<ConsumerConfig>();
var producerConfig = kafkaConfig.Get<ProducerConfig>();

//Units
builder.Services.AddSingleton<IKafkaConsumer>(sp =>
{
    var scope = sp.CreateScope();
    var messageHandlers = scope.ServiceProvider.GetServices<IMessageHandler>();
    var logger = sp.GetService<ILogger<KafkaConsumer>>();
    return new KafkaConsumer(consumerConfig, messageHandlers, logger);
});
builder.Services.AddSingleton<IKafkaProducer>(sp =>
{
    var logger = sp.GetService<ILogger<KafkaProducer>>();
    return new KafkaProducer(producerConfig, logger);
});


//Services
builder.Services.AddScoped<IKafkaProducerService, KafkaProducerService>();
builder.Services.AddHostedService<KafkaConsumerService>();
#endregion

builder.Services.AddScoped<IMessageHandler, OrderCreatedMessageHandler>();
builder.Services.AddScoped<IOrderService, OrderService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
Console.WriteLine();
app.UseHttpsRedirection();

// app.UseAuthorization();
Console.WriteLine("Test");
app.MapControllers();
app.Run();