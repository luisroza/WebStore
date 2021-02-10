using System;
using WebStore.Core.Messages;

namespace WebStore.Sales.Application.Commands
{
    public class CancelOrderCommand : Command
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        public CancelOrderCommand(Guid orderId, Guid customerId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}
