using System;
using WebStore.Core.DomainObjects.DTO;

namespace WebStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class StartOrderEvent : IntegrationEvent
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal Total { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public string CardExpirationDate { get; private set; }
        public string CardVerificationCode { get; private set; }
        public OrderItemList ItemList { get; private set; }

        public StartOrderEvent(Guid customerId, Guid orderId, OrderItemList itemList, decimal total, string cardName, string cardNumber, string cardExpirationDate, string cardVerificationCode)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
            ItemList = itemList;
            Total = total;
            CardName = cardName;
            CardNumber = cardNumber;
            CardExpirationDate = cardExpirationDate;
            CardVerificationCode = cardVerificationCode;
        }
    }
}
