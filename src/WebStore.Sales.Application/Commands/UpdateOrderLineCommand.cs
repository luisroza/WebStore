using FluentValidation;
using System;
using WebStore.Core.Messages;

namespace WebStore.Sales.Application.Commands
{
    public class UpdateOrderLineCommand : Command
    {
        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }

        public UpdateOrderLineCommand(Guid customerId, Guid productId, int quantity)
        {
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateOrderLineValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class UpdateOrderLineValidation : AbstractValidator<UpdateOrderLineCommand>
    {
        public UpdateOrderLineValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid customer ID");

            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid product ID");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("Minimun quantity: 1");

            //quantity limitation
            RuleFor(c => c.Quantity)
                .LessThan(5)
                .WithMessage("Maximun quantity: 5");
        }
    }
}
