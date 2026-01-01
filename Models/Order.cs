using System.ComponentModel.DataAnnotations;

namespace Online_Product_Order_API.Models
{
    public class Order
    {
        [Key]
        public int orderID { get; set; }
        public string customerName { get; set; }
        public string productName { get; set; }
        public int quantity { get; set; }

        public double price { get; set; }
        public DateTime orderdate { get; set; }
    }
}
