using System;

namespace WebStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class PaymentRejectedEvent : IntegrationEvent
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid PaymentId { get; private set; }
        public Guid TransactionId { get; private set; }
        public decimal Amount { get; private set; }

        public PaymentRejectedEvent(Guid orderId, Guid customerId, Guid paymentId, Guid transactionId, decimal amount)
        {
            AggregateId = orderId;
            OrderId = orderId;
            CustomerId = customerId;
            PaymentId = paymentId;
            TransactionId = transactionId;
            Amount = amount;
        }
    }
}
