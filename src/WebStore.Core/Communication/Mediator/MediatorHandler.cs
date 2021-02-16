using System.Threading.Tasks;
using MediatR;
using WebStore.Core.Data.EventSourcing;
using WebStore.Core.Messages;
using WebStore.Core.Messages.CommonMessages.DomainEvents;
using WebStore.Core.Messages.CommonMessages.Notifications;

namespace WebStore.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventSourcingRepository _eventSourcingRepository;

        public MediatorHandler(IMediator mediator, IEventSourcingRepository eventSourcingRepository)
        {
            _mediator = mediator;
            _eventSourcingRepository = eventSourcingRepository;
        }

        public async Task PublishEvent<T>(T events) where T : Event
        {
            await _mediator.Publish(events);
            await _eventSourcingRepository.SaveEvent(events);
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

        public async Task PublishDomainEvent<T>(T notification) where T : DomainEvent
        {
            await _mediator.Publish(notification);
        }
    }
}