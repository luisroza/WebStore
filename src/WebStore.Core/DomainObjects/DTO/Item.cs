using System;
using System.Collections.Generic;

namespace WebStore.Core.DomainObjects.DTO
{
    public class Item
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderItemList
    {
        public Guid OrderId { get; set; }
        public ICollection<Item> Lines { get; set; }
    }
}
