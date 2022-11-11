using AutoMapper;
using Producer.Dtos;
using Producer.Model;
using RabbitMQService.Dtos;
using RabbitMQService.Model;

namespace Producer.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            ConfigureMappings();
        }

        public void ConfigureMappings()
        {            
            CreateMap<Order, OrderDto>().ReverseMap();            
            CreateMap<User, UserDto>().ReverseMap();            
        }


    }
}
