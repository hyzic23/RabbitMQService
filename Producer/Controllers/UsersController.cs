using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Producer.Data;
using Producer.Dtos;
using Producer.IRepository;
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
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UsersController(IMessageProducer messageProducer,
                                IOrderDbContext context,
                                IValidator<UserDto> validator,
                                IUserRepository userRepository,
                                IMapper mapper)
        {
            this.messageProducer = messageProducer;
            this.context = context;
            this.validator = validator;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {           
            var result = userRepository.GetAllUsers();
            var orders = mapper.Map<IEnumerable<User>>(result);
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

            var user = mapper.Map<User>(userDto);           
            var response = userRepository.AddUser(user);   
            messageProducer.SendMessage(response);
            return Ok(response);
        }


    }
}
