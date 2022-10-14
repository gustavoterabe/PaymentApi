using FluentValidation;
using Payment.Domain.Models;

namespace Payment.Application.Validators;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator ()
    {
        RuleFor(s => s.SaleProductGroups).Must(list => list.Count > 0)
            .WithMessage("Sale must contain at least one product!");
    }
}
