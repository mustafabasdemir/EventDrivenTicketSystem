namespace TicketSaleAPI.Models
{
    public class TicketSaleEvent
    {
        public string EventName { get; set; }
        public int TicketCount { get; set; }
        public string BuyerName { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
