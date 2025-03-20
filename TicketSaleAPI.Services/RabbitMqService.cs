using Serilog;
using RabbitMQ.Client;
using System.Text;

namespace TicketSaleAPI.Services
{
    public class RabbitMqService
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "ticketQueue";

        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };

            try
            {
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: _queueName,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "",
                                         routingKey: _queueName,
                                         basicProperties: null,
                                         body: body);

                    // Loglama
                    Log.Information("Message sent to RabbitMQ: {Message}", message);
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda loglama
                Log.Error("Error sending message to RabbitMQ: {Error}", ex.Message);
            }
        }
    }
}
