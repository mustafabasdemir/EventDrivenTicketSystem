namespace TicketSaleAPI.Models
{
    public class TicketSale
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public int TicketCount { get; set; }
        public string BuyerName { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
