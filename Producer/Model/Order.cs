using System.ComponentModel.DataAnnotations;

namespace RabbitMQService.Model
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
