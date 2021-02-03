using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace WebStore.Sales.Application.Events
{
    public class OrderEventHandler :
        INotificationHandler<DraftOrderStartedEvent>,
        INotificationHandler<AddOrderLineEvent>,
        INotificationHandler<UpdateOrderEvent>
    {
        public Task Handle(DraftOrderStartedEvent notification, CancellationToken cancellationToken)
        {
            //TODO: When needed use this to throw some event
            return Task.CompletedTask;
        }

        public Task Handle(AddOrderLineEvent notification, CancellationToken cancellationToken)
        {
            //TODO: When needed use this to throw some event
            return Task.CompletedTask;
        }

        public Task Handle(UpdateOrderEvent notification, CancellationToken cancellationToken)
        {
            //TODO: When needed use this to throw some event
            return Task.CompletedTask;
        }
    }
}
