using FluentValidation;

public class PaymentDtoValidator : AbstractValidator<PaymentDto>
{
    public PaymentDtoValidator()
    {
        RuleFor(x => x.OrderId).GreaterThan(0).WithMessage("Order ID is required.");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Payment amount must be greater than 0.");
        RuleFor(x => x.Method).IsInEnum().WithMessage("Invalid payment method.");
    }
}
