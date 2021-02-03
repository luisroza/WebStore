using WebStore.Core.Messages;
using System;

namespace WebStore.Sales.Application.Commands
{
    public class AddOrderLineCommand : Command
    {
        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        public AddOrderLineCommand(Guid customerId, Guid productId, string nome, int quantity, decimal unityPrice)
        {
            CustomerId = customerId;
            ProductId = productId;
            Name = nome;
            Quantity = quantity;
            UnitPrice = unityPrice;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddIOrderLineValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
