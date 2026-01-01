using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Product_Order_API.Models
{
    public class BankTransaction
    {
        [Key]
        public int TransactionId { get; set; }

        public string AccountNumber { get; set; }

        public string AccountHolderName { get; set; }

        public string TransactionType { get; set; }   
        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public decimal Balance { get; set; }
    }
}
