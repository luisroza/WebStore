using WebStore.Catalog.Domain.Events;
using WebStore.Core.Communication.Mediator;
using System;
using System.Threading.Tasks;

namespace WebStore.Catalog.Domain
{
    public class StockService : IStockService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediatorHandler _mediator;

        public StockService(IProductRepository produtoRepository, 
                              IMediatorHandler mediator)
        {
            _productRepository = produtoRepository;
            _mediator = mediator;
        }

        public async Task<bool> DecreaseStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null) return false;

            if (!product.HasStock(quantity)) return false;

            product.DecreaseStock(quantity);

            // TODO: Parametrizar a Quantity de estoque baixo
            if (product.StockQuantity < 10)
            {
                await _mediator.PublishEvent(new ProductLowStockEvent(product.Id, product.StockQuantity));
            }

            _productRepository.Update(product);
            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReplenishStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null) return false;
            product.ReplenishStock(quantity);

            _productRepository.Update(product);
            return await _productRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _productRepository.Dispose();
        }
    }
}