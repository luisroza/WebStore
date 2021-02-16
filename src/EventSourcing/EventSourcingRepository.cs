using EventStore.ClientAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebStore.Core.Data.EventSourcing;
using WebStore.Core.Messages;

namespace EventSourcing
{
    public class EventSourcingRepository : IEventSourcingRepository
    {
        private readonly IEventStoreService _eventStoreService;

        public EventSourcingRepository(IEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId)
        {
            var events = await _eventStoreService.GetConnection().ReadStreamEventsBackwardAsync(aggregateId.ToString(), 0, 500, false);
            var eventList = new List<StoredEvent>();

            foreach (var solvedEvent in events.Events)
            {
                var encodedData = Encoding.UTF8.GetString(solvedEvent.Event.Data);
                var jsonData = JsonConvert.DeserializeObject<BaseEvent>(encodedData);
                
                var storedEvent = new StoredEvent(
                    solvedEvent.Event.EventId,
                    solvedEvent.Event.EventType,
                    jsonData.TimeStamp,
                    encodedData);

                eventList.Add(storedEvent);
            }
            return eventList;
        }

        public async Task SaveEvent<TEvent>(TEvent tEvent) where TEvent : Event
        {
            await _eventStoreService.GetConnection().AppendToStreamAsync(tEvent.AggregateId.ToString(), ExpectedVersion.Any, FormatEvent(tEvent));
        }

        private static IEnumerable<EventData> FormatEvent<TEvent>(TEvent tEvent) where TEvent : Event
        {
            yield return new EventData(
                Guid.NewGuid(),
                tEvent.MessageType,
                true,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(tEvent)),
                null);
        }
    }

    internal class BaseEvent
    {
        public DateTime TimeStamp { get; set; }
    }
}
