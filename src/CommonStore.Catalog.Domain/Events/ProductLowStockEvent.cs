using CommonStore.Core.Messages.CommonMessages.DomainEvents;
using System;

namespace CommonStore.Catalog.Domain.Events
{
    public class ProductLowStockEvent : DomainEvent
    {
        public int RemainingQuantity { get; private set; }

        public ProductLowStockEvent(Guid aggregateId, int remainingQuantity) : base(aggregateId)
        {
            RemainingQuantity = remainingQuantity;
        }
    }
}