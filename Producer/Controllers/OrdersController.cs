using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Producer.Data;
using Producer.IRepository;
using RabbitMQService.Dtos;
using RabbitMQService.Model;
using RabbitMQService.RabbitMQ;
using AutoMapper;

namespace RabbitMQService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderDbContext context;
        private readonly IMessageProducer messageProducer;
        private IValidator<OrderDto> validator;
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public OrdersController(IMessageProducer messageProducer,
                                IOrderDbContext context,
                                IValidator<OrderDto> validator,
                                IOrderRepository orderRepository,
                                IMapper mapper)
        {
            this.messageProducer = messageProducer;
            this.context = context;
            this.validator = validator;
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = orderRepository.GetAllOrders();
            var orders = mapper.Map<IEnumerable<Order>>(result);

            messageProducer.SendMessage(result);
            return Ok(orders);
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

            var order = mapper.Map<Order>(orderDto);
            var response = orderRepository.AddOrder(order);            

            messageProducer.SendMessage(response);            
            return Ok(response);
        }


    }
}
