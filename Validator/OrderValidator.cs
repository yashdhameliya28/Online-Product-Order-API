using FluentValidation;
using Online_Product_Order_API.Models;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(x => x.customerName)
            .NotEmpty().WithMessage("CustomerName is required");

        RuleFor(x => x.productName)
            .NotEmpty().WithMessage("ProductName is required");

        RuleFor(x => x.quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0");

        RuleFor(x => x.price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.orderdate)
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage("OrderDate must be today or earlier");
    }
}
