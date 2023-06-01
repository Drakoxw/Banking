
using Banking.Account.Command.Domain.Common;

namespace Banking.Account.Command.Aplication.Contracts.Persistence
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        Task<TDocument> GetById(string id);

        Task<IEnumerable<TDocument>> GetAll();

        Task UpdateDocument(TDocument document);

        Task InsertDocument(TDocument document);

        Task DeleteById(string id);

    }
}
