
using Banking.Cqrs.Core.Events;

namespace Banking.Cqrs.Core.Infrastructure
{
    public interface EventStore
    {
        public Task SaveEvents(string AggregateId, IEnumerable<BaseEvent> events, int versionExpected);

        public Task<List<BaseEvent>> GetEvents(string AggregateId);
    }
}
