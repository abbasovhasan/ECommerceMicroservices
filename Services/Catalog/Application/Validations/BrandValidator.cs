using FluentValidation;

public class BrandValidator : AbstractValidator<BrandDto>
{
    public BrandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Brand name is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Brand description is required.");
    }
}
