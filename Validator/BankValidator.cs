using FluentValidation;
using Online_Product_Order_API.Models;

namespace Online_Product_Order_API.Validator
{
    public class BankValidator : AbstractValidator<BankTransaction>
    {
        public BankValidator()
        {
            RuleFor(x => x.AccountNumber)
                .NotEmpty().WithMessage("AccountNumber is required")
                .Matches(@"^\d{12}$")
                .WithMessage("AccountNumber must be exactly 12 digits");

            RuleFor(x => x.AccountHolderName)
                .NotEmpty().WithMessage("AccountHolderName is required");

            RuleFor(x => x.TransactionType)
                .NotEmpty()
                .Must(t => t == "Deposit" || t == "Withdrawal")
                .WithMessage("TransactionType must be either 'Deposit' or 'Withdrawal'");

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than 0");

            RuleFor(x => x.TransactionDate)
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("TransactionDate must not be a future date");

            RuleFor(x => x)
                .Must(HasSufficientBalance)
                .WithMessage("Withdrawal amount must not exceed available balance");

            RuleFor(x => x)
                .Must(BalanceShouldNotBeNegative)
                .WithMessage("Balance must never become negative");
        }

        public bool HasSufficientBalance(BankTransaction dto)
        {
            if (dto.TransactionType == "Withdrawal")
                return dto.Amount <= dto.Balance;

            return true;
        }

        private bool BalanceShouldNotBeNegative(BankTransaction dto)
        {
            if (dto.TransactionType == "Withdrawal")
                return (dto.Balance - dto.Amount) >= 0;

            return true;
        }
    }
}
