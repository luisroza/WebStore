using System;
using WebStore.Core.Messages;

namespace WebStore.Sales.Application.Events
{
    public class VoucherAppliedOrderEvent : Event
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid VoucherId { get; private set; }

        public VoucherAppliedOrderEvent(Guid customerId, Guid orderId, Guid voucherId)
        {
            VoucherId = voucherId;
            CustomerId = customerId;
            OrderId = orderId;
        }
    }
}
