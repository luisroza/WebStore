using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Core.Communication.Mediator;
using WebStore.Core.Messages.CommonMessages.IntegrationEvents;

namespace WebStore.Catalog.Domain.Events
{
    public class ProductEventHandler : INotificationHandler<ProductLowStockEvent>,
                                       INotificationHandler<StartOrderEvent>,
                                       INotificationHandler<OrderCancelledEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMediatorHandler _mediatorHandler;

        public ProductEventHandler(IProductRepository productRepository, IStockService stockService, IMediatorHandler mediatorHandler)
        {
            _productRepository = productRepository;
            _stockService = stockService;
            _mediatorHandler = mediatorHandler;
        }

        public async Task Handle(ProductLowStockEvent message, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(message.AggregateId);

            // TODO: Send an e-mail to the team warning them to buy more products
        }

        public async Task Handle(StartOrderEvent message, CancellationToken cancellationToken)
        {
            var result = await _stockService.DecreaseStockProductList(message.ItemList);

            if (result)
            {
                await _mediatorHandler.PublishEvent(new OrderStockConfirmedEvent(message.OrderId, message.CustomerId, message.ItemList,
                    message.Total, message.CardName, message.CardNumber, message.CardExpirationDate, message.CardVerificationCode)); ;
            }
            else
            {
                await _mediatorHandler.PublishEvent(new OrderStockRejectedEvent(message.OrderId, message.CustomerId));
            }
        }

        public async Task Handle(OrderCancelledEvent message, CancellationToken cancellationToken)
        {
            await _stockService.ReplenishStockOrderProductsList(message.OrderProducts);
        }
    }
}