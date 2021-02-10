using WebStore.Catalog.Domain.Events;
using WebStore.Core.Communication.Mediator;
using System;
using System.Threading.Tasks;
using WebStore.Core.DomainObjects.DTO;
using WebStore.Core.Messages.CommonMessages.Notifications;

namespace WebStore.Catalog.Domain
{
    public class StockService : IStockService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public StockService(IProductRepository produtoRepository, 
                              IMediatorHandler mediatorHandler)
        {
            _productRepository = produtoRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> DecreaseStock(Guid productId, int quantity)
        {
            if (!await DecreaseStockItem(productId, quantity)) return false;
            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> DecreaseStockProductList(OrderItemList list)
        {
            foreach (var item in list.Lines)
            {
                if (!await DecreaseStockItem(item.Id, item.Quantity)) return false;
            }
            return await _productRepository.UnitOfWork.Commit();
        }

        private async Task<bool> DecreaseStockItem(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);
            if (product == null) return false;

            if (!product.HasStock(quantity))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Stock", $"Order - {product.Name} has no stock"));
                return false;
            }

            product.DecreaseStock(quantity);

            //Business Rule -> place it into a config file
            if(product.StockQuantity < 10)
            {
                await _mediatorHandler.PublishEvent(new ProductLowStockEvent(product.Id, product.StockQuantity));
            }

            _productRepository.Update(product);
            return true;
        }

        public async Task<bool> ReplenishStock(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null) return false;
            product.ReplenishStock(quantity);

            _productRepository.Update(product);
            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReplenishStockOrderProductsList(OrderItemList list)
        {
            foreach (var item in list.Lines)
            {
                await ReplenishStock(item.Id, item.Quantity);
            }
            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReplenishStockItem(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null) return false;
            product.ReplenishStock(quantity);

            _productRepository.Update(product);
            return true;
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}