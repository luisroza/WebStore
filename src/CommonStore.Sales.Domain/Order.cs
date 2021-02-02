using CommonStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonStore.Sales.Domain
{
    public class Order : Entity, IAggregateRoot
    {
        public int Code { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool VoucherUsed { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalPrice { get; private set; }
        public DateTime CreateDate { get; private set; }
        public OrderStatus OrderStatus { get; private set; }

        //Class use only
        private readonly List<OrderLine> _orderLines;

        //public reading of _orderLines
        public IReadOnlyCollection<OrderLine> OrderLines => _orderLines;

        //EF relation
        public virtual Voucher Voucher { get; private set; }

        public Order(Guid customerId, bool voucherUsed, decimal discount, decimal totalPrice)
        {
            CustomerId = customerId;
            VoucherUsed = voucherUsed;
            Discount = discount;
            TotalPrice = totalPrice;
            _orderLines = new List<OrderLine>();
        }

        //EF Relation
        protected Order()
        {
            _orderLines = new List<OrderLine>();
        }

        public void CalculateTotalPriceDiscount()
        {
            if (!VoucherUsed) return;

            decimal discount = 0;
            var valor = TotalPrice;

            if (Voucher.TypeDiscountVoucher == VoucherType.Percentage)
            {
                if(Voucher.Percentage.HasValue)
                {
                    Discount = (valor * Voucher.Percentage.Value) / 100;
                    valor -= Discount;
                }
            }
            else
            {
                if(Voucher.PriceDiscount.HasValue)
                {
                    Discount = Voucher.PriceDiscount.Value;
                    valor -= Discount;
                }
            }

            TotalPrice = valor < 0 ? 0 : valor;
            Discount = discount;
        }

        public void CalculateOrderPrice()
        {
            TotalPrice = OrderLines.Sum(p => p.CalculatePrice());
            CalculateTotalPriceDiscount();
        }

        public void ApplyVoucher(Voucher voucher)
        {
            Voucher = voucher;
            VoucherUsed = true;
            CalculateOrderPrice();
        }

        public bool ExistentOrderLine(OrderLine item)
        {
            return _orderLines.Any(p => p.ProductId== item.ProductId);
        }

        public void AddOrderLine(OrderLine item)
        {
            if (item.IsValid()) return;

            item.AssociateOrder(Id);

            if(ExistentOrderLine(item))
            {
                var existentOrderLine = _orderLines.FirstOrDefault(p => p.ProductId == item.ProductId);
                existentOrderLine.AddUnit(item.Quantity);
                item = existentOrderLine;

                _orderLines.Remove(existentOrderLine);
            }

            item.CalculatePrice();
            _orderLines.Add(item);

            CalculateOrderPrice();
        }

        public void RemoveOrderLine(OrderLine item)
        {
            if (!item.IsValid()) return;

            var existentOrderLine = OrderLines.FirstOrDefault(p => p.ProductId == item.ProductId);

            if (existentOrderLine == null)
                throw new DomainException("This order line does not belong to the order");
            _orderLines.Remove(existentOrderLine);

            CalculateOrderPrice();
        }

        public void UpdateOrderLine(OrderLine item)
        {
            if (!item.IsValid()) return;
            item.AssociateOrder(Id);

            var existentOrderLine = OrderLines.FirstOrDefault(p => p.ProductId == item.ProductId);

            if (existentOrderLine == null)
                throw new DomainException("This order line does not belong to the order");

            _orderLines.Remove(existentOrderLine);
            _orderLines.Add(item);

            CalculateOrderPrice();
        }

        public void AddUnits(OrderLine item, int units)
        {
            item.UpdateUnit(units);
            UpdateOrderLine(item);
        }

        public void MakeDraft() => OrderStatus = OrderStatus.Draft;

        public void StarOrder() => OrderStatus = OrderStatus.Started;

        public void FinalizeOrder() => OrderStatus = OrderStatus.Paid;

        public void CancelOrder() => OrderStatus = OrderStatus.Cancelled;

        public static class OrderFactory
        {
            public static Order NewDraftOrder(Guid customerId)
            {
                var order = new Order { CustomerId = customerId };
                order.MakeDraft();
                return order;
            }
        }
    }
}