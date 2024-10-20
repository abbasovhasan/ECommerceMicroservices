using FluentValidation;

public class BasketItemDtoValidator : AbstractValidator<BasketItemDto>
{
    public BasketItemDtoValidator()
    {
        // Ürün ID'si sıfırdan büyük olmalı
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("Product ID must be greater than 0.");

        // Miktar sıfırdan büyük olmalı
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

        // Birim fiyat sıfırdan büyük olmalı
        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than 0.");
    }
}
