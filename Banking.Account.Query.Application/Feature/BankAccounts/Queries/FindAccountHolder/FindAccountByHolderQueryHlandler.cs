
using Banking.Account.Query.Application.Contracts.Persistence;
using Banking.Account.Query.Domain;
using MediatR;

namespace Banking.Account.Query.Application.Feature.BankAccounts.Queries.FindAccountHolder
{
    public class FindAccountByHolderQueryHlandler : IRequestHandler<FindAccountByHolderQuery, IEnumerable<BankAccount>>
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public FindAccountByHolderQueryHlandler(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<IEnumerable<BankAccount>> Handle(FindAccountByHolderQuery request, CancellationToken cancellationToken)
        {
            return await _bankAccountRepository.FindByAccountHolder(request.AccountHolder);
        }
    }
}
