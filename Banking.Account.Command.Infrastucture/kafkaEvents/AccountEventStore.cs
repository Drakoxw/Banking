
using Banking.Account.Command.Aplication.Aggregates;
using Banking.Account.Command.Aplication.Contracts.Persistence;
using Banking.Account.Command.Domain;
using Banking.Cqrs.Core.Events;
using Banking.Cqrs.Core.Infrastructure;
using Banking.Cqrs.Core.Producers;

namespace Banking.Account.Command.Infrastucture.kafkaEvents
{
    public class AccountEventStore : EventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly EventProducer _eventProducer;

        public AccountEventStore(IEventStoreRepository eventStoreRepository, EventProducer eventProducer)
        {
            _eventStoreRepository = eventStoreRepository;
            _eventProducer = eventProducer;
        }

        public async Task<List<BaseEvent>> GetEvents(string AggregateId)
        {
            var eventStream = await _eventStoreRepository.FindByAggregateIdentifier(AggregateId);
            if (eventStream == null || !eventStream.Any())
            {
                throw new Exception("La cuenta bancaria no existe");
            }
            return eventStream.Select(x => x.EventData).ToList();

        }

        public async Task SaveEvents(string AggregateId, IEnumerable<BaseEvent> events, int versionExpected)
        {
            var eventStream = await _eventStoreRepository.FindByAggregateIdentifier(AggregateId);
            if (versionExpected != -1 && eventStream.ElementAt(eventStream.Count() - 1).Version != versionExpected)
            {
                throw new Exception("Errores de concurrencia");
            }

            var version = versionExpected;
            foreach (var ev in events)
            {
                version++;
                ev.Version = version;
                var eventModel = new EventModel
                {
                    TimeStamp = DateTime.Now,
                    AggregateIdentifier = AggregateId,
                    AggregateType = nameof(AccountAggregate),
                    Version = version,
                    EventType = ev.GetType().ToString(),
                    EventData = ev
                };

                await _eventStoreRepository.InsertDocument(eventModel);
                _eventProducer.Produce(ev.GetType().Name, ev);
            }
        }
    }
}
