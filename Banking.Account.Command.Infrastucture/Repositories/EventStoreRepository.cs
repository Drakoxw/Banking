
using Banking.Account.Command.Aplication.Contracts.Persistence;
using Banking.Account.Command.Aplication.Models;
using Banking.Account.Command.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Banking.Account.Command.Infrastucture.Repositories
{
    public class EventStoreRepository : MongoRepository<EventModel>, IEventStoreRepository
    {
        public EventStoreRepository(IOptions<MongoSettings> options) : base(options)
        {
        }

        public async Task<IEnumerable<EventModel>> FindByAggregateIdentifier(string AggregateId)
        {
            var filter = Builders<EventModel>.Filter.Eq(evn => evn.AggregateIdentifier, AggregateId);
            return await collection.Find(filter).ToListAsync();
        }
    }
}
