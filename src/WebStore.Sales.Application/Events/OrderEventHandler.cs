using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Core.Communication.Mediator;
using WebStore.Core.Messages.CommonMessages.IntegrationEvents;
using WebStore.Sales.Application.Commands;

namespace WebStore.Sales.Application.Events
{
    public class OrderEventHandler :
        INotificationHandler<DraftOrderStartedEvent>,
        INotificationHandler<AddOrderLineEvent>,
        INotificationHandler<UpdateOrderEvent>,
        INotificationHandler<OrderStockRejectedEvent>,
        INotificationHandler<CheckOutEvent>,
        INotificationHandler<PaymentRejectedEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public OrderEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public Task Handle(DraftOrderStartedEvent message, CancellationToken cancellationToken)
        {
            //TODO: When needed use this to throw some event
            return Task.CompletedTask;
        }

        public Task Handle(AddOrderLineEvent message, CancellationToken cancellationToken)
        {
            //TODO: When needed use this to throw some event
            return Task.CompletedTask;
        }

        public Task Handle(UpdateOrderEvent message, CancellationToken cancellationToken)
        {
            //TODO: When needed use this to throw some event
            return Task.CompletedTask;
        }

        public Task Handle(OrderStockRejectedEvent message, CancellationToken cancellationToken)
        {
            //cancel order process - show error to customer
            return Task.CompletedTask;
        }

        public Task Handle(CheckOutEvent message, CancellationToken cancellationToken)
        {
            //Order completed
            await _mediatorHandler.SendCommand(new FinalizeOrderCommand(message.CustomerId, message.CustomerId));
        }

        public Task Handle(PaymentRejectedEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.SendCommand(new CancelOrderReplenishStockCommand(message.OrderId, message.CustomerId));
        }
    }
}
