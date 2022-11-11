using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Producer.Data;
using RabbitMQService.Dtos;
using RabbitMQService.Model;
using RabbitMQService.RabbitMQ;

namespace RabbitMQService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderDbContext context;
        private readonly IMessageProducer messageProducer;
        private IValidator<OrderDto> validator;

        public OrdersController(IMessageProducer messageProducer,
                                IOrderDbContext context,
                                IValidator<OrderDto> validator)
        {
            this.messageProducer = messageProducer;
            this.context = context;
            this.validator = validator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = context.Order.ToList();
            messageProducer.SendMessage(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            ValidationResult result = await validator.ValidateAsync(orderDto);
            if (!result.IsValid)
            {
                result.Errors.ToList();
                return Ok(result);              
            }

            Order order = new()
            {
                ProductName = orderDto.ProductName,
                Price = orderDto.Price,
                Quantity = orderDto.Quantity
            };

            context.Order.Add(order);
            await context.SaveChangesAsync();

            messageProducer.SendMessage(order);
            return Ok(new { id = order.Id});
        }


    }
}
