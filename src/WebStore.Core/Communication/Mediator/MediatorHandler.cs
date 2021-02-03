using System.Threading.Tasks;
using MediatR;
using WebStore.Core.Messages;
using WebStore.Core.Messages.CommonMessages.Notifications;

namespace WebStore.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T events) where T : Event
        {
            await _mediator.Publish(events);
        }

        public async Task<bool> SendCommand<T>(T command) where T : Command
        {
            //Send = request
            return await _mediator.Send(command);
        }

        public async Task PublishNotification<T>(T notification) where T : DomainNotification
        {
            await _mediator.Publish(notification);
        }
    }
}