using AutoMapper;
using Producer.Dtos;
using Producer.Model;
using Producer.Profiles;
using RabbitMQService.Dtos;
using RabbitMQService.Model;

namespace Producer.Configuration
{
    public static class CustomAutoMapper
    {
        public static void AddCustomConfigurationAutoMapper(this IServiceCollection services)
        {            
            var config = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new AutoMapperProfile());

                cfg.CreateMap<Order, OrderDto>().ReverseMap();
                cfg.CreateMap<User, UserDto>().ReverseMap();
            });

           
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
