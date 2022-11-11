using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Producer.Configuration;
using Producer.Data;
using Producer.Dtos;
using Producer.IRepository;
using Producer.Profiles;
using Producer.Repository;
using Producer.Validator;
using RabbitMQService.Dtos;
using RabbitMQService.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<OrderDbContext>(item => item.UseSqlServer(builder.Configuration.GetConnectionString("OrderDbContext")));
builder.Services.AddScoped<IOrderDbContext, OrderDbContext>();
builder.Services.AddScoped<IMessageProducer, RabbitMQProducer>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IValidator<OrderDto>, OrderValidator>();
builder.Services.AddScoped<IValidator<UserDto>, UserValidator>();

builder.Services.AddCustomConfigurationAutoMapper();


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
