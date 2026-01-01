using Microsoft.EntityFrameworkCore;
using Online_Product_Order_API.Models;

namespace Online_Product_Order_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> orders { get; set; }
        public DbSet<BankTransaction> bankTransactions { get; set; }  
    }
}
