using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TicketSaleAPI.Models;  // TicketSale sınıfını ekliyoruz

namespace TicketSaleAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public DbSet<TicketSale> TicketSales { get; set; }
    }
}
