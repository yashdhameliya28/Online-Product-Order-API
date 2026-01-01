using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Product_Order_API.Data;
using Online_Product_Order_API.Models;

namespace Online_Product_Order_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IValidator<BankTransaction> _validator;
        public BankController(AppDbContext context, IValidator<BankTransaction> validator)
        {
            _context = context;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> addTransaction(BankTransaction bankTransaction)
        {
            var result = await _validator.ValidateAsync(bankTransaction);
            if (!result.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    errors = result.Errors.Select(e => e.ErrorMessage)
                });
            }
            var newTxn = new BankTransaction
            {
                TransactionDate = bankTransaction.TransactionDate,
                TransactionId = bankTransaction.TransactionId,
                AccountHolderName = bankTransaction.AccountHolderName,
                AccountNumber = bankTransaction.AccountNumber,
                Amount = bankTransaction.Amount,
                Balance = bankTransaction.Balance,
                TransactionType = bankTransaction.TransactionType,
            };

            _context.bankTransactions.Add(newTxn);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Transaction added..." });
        }
    }
}
