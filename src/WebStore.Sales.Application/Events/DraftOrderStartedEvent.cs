using WebStore.Core.Messages;
using System;

namespace WebStore.Sales.Application.Events
{
    public class DraftOrderStartedEvent : Event
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        public DraftOrderStartedEvent(Guid customerId, Guid orderId)
        {
            CustomerId = customerId;
            OrderId = orderId;
            AggregateId = orderId;
        }
    }
}
