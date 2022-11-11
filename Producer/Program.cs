using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Producer.Data;
using Producer.Dtos;
using Producer.Validator;
using RabbitMQService.Dtos;
using RabbitMQService.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<OrderDbContext>(options =>
//    options.UseInMemoryDatabase("ASPNETCoreRabbitMQ"));

// Add services to the container.
builder.Services.AddDbContext<OrderDbContext>(item => item.UseSqlServer(builder.Configuration.GetConnectionString("OrderDbContext")));
builder.Services.AddScoped<IOrderDbContext, OrderDbContext>();
builder.Services.AddScoped<IMessageProducer, RabbitMQProducer>();

builder.Services.AddScoped<IValidator<OrderDto>, OrderValidator>();
builder.Services.AddScoped<IValidator<UserDto>, UserValidator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
