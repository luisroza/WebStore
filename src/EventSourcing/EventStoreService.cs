using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;

namespace EventSourcing
{
    public class EventStoreService : IEventStoreService
    {
        private readonly IEventStoreConnection _connection;

        //EventStore uses 1 connection per application not per user, singleton scope
        public EventStoreService(IConfiguration configuration)
        {
            _connection = EventStoreConnection.Create(configuration.GetConnectionString("EventStoreConnection"));
            _connection.ConnectAsync();
        }

        public IEventStoreConnection GetConnection()
        {
            return _connection;
        }
    }
}
