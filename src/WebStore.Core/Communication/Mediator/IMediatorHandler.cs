using System.Threading.Tasks;
using WebStore.Core.Messages;
using WebStore.Core.Messages.CommonMessages.DomainEvents;
using WebStore.Core.Messages.CommonMessages.Notifications;

namespace WebStore.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T events) where T : Event;
        Task<bool> SendCommand<T>(T command) where T : Command;
        Task PublishNotification<T>(T notification) where T : DomainNotification;
        Task PublishDomainEvent<T>(T domainEvent) where T : DomainEvent;
    }
}