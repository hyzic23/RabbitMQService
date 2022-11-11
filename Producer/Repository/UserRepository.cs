using Producer.Data;
using Producer.IRepository;
using Producer.Model;
using RabbitMQService.Model;

namespace Producer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IOrderDbContext _orderDbContext;

        public UserRepository(IOrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public User AddUser(User user)
        {
            _orderDbContext.Users.Add(user);
            _orderDbContext.SaveChangesAsync();
            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _orderDbContext.Users.ToList();
        }
    }
}
