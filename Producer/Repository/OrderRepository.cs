using Producer.Data;
using Producer.IRepository;
using Producer.Model;
using RabbitMQService.Model;

namespace Producer.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IOrderDbContext _orderDbContext;

        public OrderRepository(IOrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public Order AddOrder(Order order)
        {
            _orderDbContext.Order.Add(order);
            _orderDbContext.SaveChangesAsync();
            return order;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orderDbContext.Order.ToList();
        }
    }
}
