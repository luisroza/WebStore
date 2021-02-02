using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CommonStore.Catalog.Domain.Events
{
    public class ProductEventHandler : INotificationHandler<ProductLowStockEvent>
    {
        private readonly IProductRepository _productRepository;

        public ProductEventHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(ProductLowStockEvent message, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(message.AggregateId);

            // Enviar um email para aquisicao de mais produtos.
        }
    }
}