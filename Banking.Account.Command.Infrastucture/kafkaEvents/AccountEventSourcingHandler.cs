
using Banking.Account.Command.Aplication.Aggregates;
using Banking.Cqrs.Core.Domain;
using Banking.Cqrs.Core.Handlers;

namespace Banking.Account.Command.Infrastucture.kafkaEvents
{
    public class AccountEventSourcingHandler : EventSourcingHandler<AccountAggregate>
    {
        private readonly AccountEventStore _eventStore;

        public AccountEventSourcingHandler(AccountEventStore eventStore)
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
