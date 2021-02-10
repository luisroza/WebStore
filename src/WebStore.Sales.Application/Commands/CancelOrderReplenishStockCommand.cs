using System;
using WebStore.Core.Messages;

namespace WebStore.Sales.Application.Commands
{
    public class CancelOrderReplenishStockCommand : Command
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        public CancelOrderReplenishStockCommand(Guid orderId, Guid customerId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}
