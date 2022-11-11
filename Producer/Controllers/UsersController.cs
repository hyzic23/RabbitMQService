using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Producer.Data;
using Producer.Dtos;
using Producer.Model;
using RabbitMQService.Dtos;
using RabbitMQService.Model;
using RabbitMQService.RabbitMQ;

namespace RabbitMQService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IOrderDbContext context;
        private readonly IMessageProducer messageProducer;
        private IValidator<UserDto> validator;

        public UsersController(IMessageProducer messageProducer,
                                IOrderDbContext context,
                                IValidator<UserDto> validator)
        {
            this.messageProducer = messageProducer;
            this.context = context;
            this.validator = validator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = context.Users.ToList();
            messageProducer.SendMessage(result);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            ValidationResult result = await validator.ValidateAsync(userDto);
            if (!result.IsValid)
            {
                result.Errors.ToList();
                return Ok(result);              
            }

            User user = new()
            {
                Username = userDto.Username,
                Password = userDto.Password
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            messageProducer.SendMessage(user);
            return Ok(new { id = user.Id});
        }


    }
}
