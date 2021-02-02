using FluentValidation;
using System;

namespace CommonStore.Sales.Application.Commands
{
    public class AddIOrderLineValidation : AbstractValidator<AddOrderLineCommand>
    {
        public AddIOrderLineValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid customer ID");

            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid product ID");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Product's name not given");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("Minimun quantity: 1");

            //quantity limitation
            RuleFor(c => c.Quantity)
                .LessThan(5)
                .WithMessage("Maximun quantity: 5");

            RuleFor(c => c.UnitPrice)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero");
        }
    }
}
