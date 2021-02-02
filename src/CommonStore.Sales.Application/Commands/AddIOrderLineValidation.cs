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
                .WithMessage("ID do cliente inválido");

            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("ID do produto inválido");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Nome do produto não foi informado");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity mínima de um item é 1");

            RuleFor(c => c.Quantity)
                .LessThan(15)
                .WithMessage("Quantity máxima de um item é 15");

            RuleFor(c => c.UnitPrice)
                .GreaterThan(0)
                .WithMessage("O valor do item precisa ser maior que 0");
        }
    }
}
