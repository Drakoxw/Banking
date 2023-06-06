
using Banking.Account.Command.Aplication.Aggregates;
using Banking.Cqrs.Core.Domain;
using Banking.Cqrs.Core.Handlers;
using Banking.Cqrs.Core.Infrastructure;

namespace Banking.Account.Command.Infrastucture.kafkaEvents
{
    public class AccountEventSourcingHandler : EventSourcingHandler<AccountAggregate>
    {
        private readonly EventStore _eventStore;

        public AccountEventSourcingHandler(EventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<AccountAggregate> GetById(string id)
        {
            var aggregate = new AccountAggregate();
            var events = await _eventStore.GetEvents(id);

            if (events != null && events.Any())
            {
                aggregate.ReplayEvents(events);
                var lastestVersion = events.Max(x => x.Version);
                aggregate.SetVersion(lastestVersion);
            }

            return aggregate;
        }

        public async Task Save(AggregateRoot aggregate)
        {
            await _eventStore.SaveEvents(aggregate.Id, aggregate.GetUnCommitedChanges(), aggregate.GetVersion());
            aggregate.MarkChangesAsCommited();
        }
    }
}
