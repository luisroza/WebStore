using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Core.Messages;

namespace WebStore.Core.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {
        Task SaveEvent<TEvent>(TEvent tEvent) where TEvent : Event;
        Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId);
    }
}
