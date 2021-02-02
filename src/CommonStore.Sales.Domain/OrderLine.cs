using CommonStore.Core.DomainObjects;
using System;

namespace CommonStore.Sales.Domain
{
    public class OrderLine : Entity
    {
        public OrderLine(Guid productId, string productName, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        protected OrderLine() { }
        
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        //EF Relation
        public Order Order { get; set; }

        internal void AssociateOrder(Guid orderId)
        {
            OrderId = orderId;
        }

        public decimal CalculatePrice()
        {
            return Quantity * UnitPrice;
        }

        internal void AddUnit(int units)
        {
            Quantity += units;
        }

        internal void UpdateUnit(int units)
        {
            Quantity = units;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
