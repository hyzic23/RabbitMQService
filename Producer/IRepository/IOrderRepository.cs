using Producer.Model;
using RabbitMQService.Model;

namespace Producer.IRepository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrders();
        Order AddOrder(Order order);
    }
}
