using System;
using System.Collections.Generic;

namespace WebStore.Payments.Business
{
    public class Order
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public List<Product> Products { get; set; }
    }
}
