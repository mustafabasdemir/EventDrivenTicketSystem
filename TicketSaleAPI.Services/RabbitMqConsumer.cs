using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using TicketSaleAPI.Models;
using System.Text;
using Newtonsoft.Json;

namespace TicketSaleAPI.Services
{
    public class RabbitMqConsumer
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "ticketQueue";

        public void StartConsuming()
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, e) =>
                {
                    var message = Encoding.UTF8.GetString(e.Body.ToArray());
                    // Mesajı JSON'dan TicketSaleEvent'e dönüştür
                    var ticketSaleEvent = JsonConvert.DeserializeObject<TicketSaleEvent>(message);

                    // Event'i işle
                    ProcessTicketSaleEvent(ticketSaleEvent);
                };

                channel.BasicConsume(queue: _queueName,
                                     autoAck: true,
                                     consumer: consumer);

                
                Console.WriteLine("Consumer is listening for events...");
                Console.ReadLine();
            }
        }

        private void ProcessTicketSaleEvent(TicketSaleEvent ticketSaleEvent)
        {
            // Loglama: Gelen event logla
            Log.Information("Received Ticket Sale Event: {EventName} for {TicketCount} tickets",
                ticketSaleEvent.EventName, ticketSaleEvent.TicketCount);

           
        }
    }
}
