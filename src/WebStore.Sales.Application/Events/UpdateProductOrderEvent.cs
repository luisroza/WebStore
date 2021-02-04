using WebStore.Core.Messages;
using System;

namespace WebStore.Sales.Application.Events
{
    public class UpdateProductOrderEvent : Event
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }

        public UpdateProductOrderEvent(Guid customerId, Guid orderId, Guid productId, int quantity)
        {
            AggregateId = orderId;
            ProductId = productId;
            CustomerId = customerId;
            OrderId = orderId;
            Quantity = quantity;
        }
    }
}
