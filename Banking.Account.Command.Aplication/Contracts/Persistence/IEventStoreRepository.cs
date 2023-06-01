using Banking.Account.Command.Domain;

namespace Banking.Account.Command.Aplication.Contracts.Persistence
{
    public interface IEventStoreRepository: IMongoRepository<EventModel>
    {
        Task<IEnumerable<EventModel>> FindByAggregateIdentifier(string AggregateId);
    }
}
