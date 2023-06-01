
using Banking.Account.Command.Aplication.Contracts.Persistence;
using Banking.Account.Command.Aplication.Models;
using Banking.Account.Command.Domain.Common;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Banking.Account.Command.Infrastucture.Repositories
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        protected readonly IMongoCollection<TDocument> collection;

        public MongoRepository(IOptions<MongoSettings> options)
        {
            var client = new MongoClient(options.Value.ConectionString);
            var db = client.GetDatabase(options.Value.Database);
            collection = db.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            #pragma warning disable CS8600
            #pragma warning disable CS8602
            return ((BsonCollectionAttribute)documentType
                    .GetCustomAttributes(typeof(BsonCollectionAttribute), true)
                    .FirstOrDefault()).CollectionName;
            #pragma warning restore CS8602
        }

        public async Task DeleteById(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            await collection.FindOneAndDeleteAsync(filter);
        }

        public async Task<IEnumerable<TDocument>> GetAll()
        {
           return await collection.Find(p => true).ToListAsync();
        }

        public async Task<TDocument> GetById(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            return await collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task InsertDocument(TDocument document)
        {
            await collection.InsertOneAsync(document);
        }

        public async Task UpdateDocument(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await collection.FindOneAndReplaceAsync(filter, document);
        }
    }
}
