using WebStore.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace WebStore.Sales.Domain
{
    public class Voucher : Entity
    {
        public string Code { get; private set; }
        public decimal? Percentage { get; private set; }
        public decimal? PriceDiscount { get; private set; }
        public int Quantity{ get; private set; }
        public VoucherType TypeDiscountVoucher { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public DateTime? UsedDate { get; private set; }
        public DateTime? ExpirationDate { get; private set; }
        public bool Active { get; private set; }
        public bool Used { get; private set; }

        //EF Relation
        public ICollection<Order> Orders { get; set; }
    }
}
