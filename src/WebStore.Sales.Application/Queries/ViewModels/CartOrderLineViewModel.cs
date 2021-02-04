using System;

namespace WebStore.Sales.Application.Queries.ViewModels
{
    public class CartOrderLineViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
