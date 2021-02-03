using System;
using System.Threading.Tasks;

namespace WebStore.Catalog.Domain
{
    public interface IStockService : IDisposable
    {
        Task<bool> DecreaseStock(Guid productId, int Quantity);
        Task<bool> ReplenishStock(Guid productId, int Quantity);
    }
}