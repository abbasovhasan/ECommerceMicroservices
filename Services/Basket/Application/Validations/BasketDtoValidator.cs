using FluentValidation;

public class BasketDtoValidator : AbstractValidator<BasketDto>
{
    public BasketDtoValidator()
    {
        // CustomerId boş olamaz
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required.")
            .Length(1, 50).WithMessage("Customer ID length must be between 1 and 50 characters.");

        // Toplam fiyat sıfırdan büyük olmalı
        RuleFor(x => x.TotalPrice)
            .GreaterThan(0).WithMessage("Total price must be greater than 0.");

        // Sepet öğeleri boş olamaz ve her öğe doğrulanmalıdır
        RuleForEach(x => x.Items)
            .SetValidator(new BasketItemDtoValidator()); // Her bir BasketItemDto doğrulanır
    }
}
