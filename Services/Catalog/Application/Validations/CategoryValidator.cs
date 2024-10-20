using FluentValidation;

public class CategoryValidator : AbstractValidator<CategoryDto>
{
    public CategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Category name is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Category description is required.");
    }
}
