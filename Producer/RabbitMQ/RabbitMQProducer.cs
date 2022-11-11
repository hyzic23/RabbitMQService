using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQService.RabbitMQ
{
    public class RabbitMQProducer : IMessageProducer
    {
        public void SendMessage<T>(T message)
        {
            //Create a connection to the RabbitMQ Server
            var factory = new ConnectionFactory
            { 
                HostName = "localhost"
            };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();   //channel allows us to interact  with the RabbitMQ APIs

            //with connection created, we can now declare a queue
            channel.QueueDeclare("orders", exclusive: false);   // this creates a queue on the server with the name orders

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "orders", body: body);
        }
    }
}
