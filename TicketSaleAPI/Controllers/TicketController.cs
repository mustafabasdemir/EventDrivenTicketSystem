using Microsoft.AspNetCore.Mvc;
using TicketSaleAPI.Services;
using TicketSaleAPI.Models;  
namespace TicketSaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly RabbitMqService _rabbitMqService;

        public TicketController(RabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        // Bilet Satışı Başlatma
        [HttpPost("sell")]
        public IActionResult SellTicket([FromBody] TicketSaleEvent ticketSaleEvent)  // TicketSaleEvent tipi ile güncelliyoruz
        {
            // Mesajı RabbitMQ'ya gönderme
            var message = Newtonsoft.Json.JsonConvert.SerializeObject(ticketSaleEvent);  
            _rabbitMqService.SendMessage(message);

            return Ok(new { message = "Ticket sale request sent to RabbitMQ." });
        }
    }
}
