using System;
using System.Threading.Tasks;
using WebStore.Core.DomainObjects.DTO;

namespace WebStore.Catalog.Domain
{
    public interface IStockService : IDisposable
    {
        Task<bool> DecreaseStock(Guid productId, int quantity);
        Task<bool> DecreaseStockProductList(OrderItemList list);
        Task<bool> ReplenishStock(Guid productId, int quantity);
        Task<bool> ReplenishStockOrderProductsList(OrderItemList list);
    }
}