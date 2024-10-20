using FluentValidation;

public class OrderDtoValidator : AbstractValidator<OrderDto>
{
    public OrderDtoValidator()
    {
        RuleFor(x => x.CustomerId).GreaterThan(0).WithMessage("Customer ID must be greater than 0.");
        RuleFor(x => x.ShippingAddress).NotEmpty().WithMessage("Shipping address is required.");
        RuleFor(x => x.TotalAmount).GreaterThan(0).WithMessage("Total amount must be greater than 0.");
        RuleForEach(x => x.OrderItems).SetValidator(new OrderItemDtoValidator());
    }
}
