using System;

namespace WebStore.Core.Data.EventSourcing
{
    public class StoredEvent
    {
        public Guid Id { get; private set; }
        public string Type { get; private set; }
        public DateTime TimeStamp { get; set; }
        public string Data { get; private set; }

        public StoredEvent(Guid id, string type, DateTime timeStamp, string data)
        {
            Id = id;
            Type = type;
            TimeStamp = timeStamp;
            Data = data;
        }
    }
}
