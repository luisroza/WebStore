using WebStore.Core.Messages;
using System;

namespace WebStore.Sales.Application.Events
{
    public class UpdateOrderEvent : Event
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal TotalPrice { get; private set; }

        public UpdateOrderEvent(Guid customerId, Guid orderId, decimal totalPrice)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
            TotalPrice = totalPrice;
        }
    }
}
