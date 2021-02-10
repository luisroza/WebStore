using System;
using WebStore.Core.Messages;

namespace WebStore.Sales.Application.Events
{
    public class OrderFinalizedEvent : Event
    {
        public Guid OrderId { get; private set; }

        public OrderFinalizedEvent(Guid orderId)
        {
            AggregateId = orderId;
            OrderId = orderId;
        }
    }
}
