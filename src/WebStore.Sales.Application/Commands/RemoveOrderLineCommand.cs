using FluentValidation;
using System;
using WebStore.Core.Messages;

namespace WebStore.Sales.Application.Commands
{
    public class RemoveOrderLineCommand : Command
    {
        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }

        public RemoveOrderLineCommand(Guid customerId, Guid productId)
        {
            CustomerId = customerId;
            ProductId = productId;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveOrderLineValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RemoveOrderLineValidation : AbstractValidator<RemoveOrderLineCommand>
    {
        public RemoveOrderLineValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid customer ID");

            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid product ID");
        }
    }
}
