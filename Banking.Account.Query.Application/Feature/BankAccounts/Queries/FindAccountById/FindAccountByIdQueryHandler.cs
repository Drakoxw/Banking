

using Banking.Account.Query.Application.Contracts.Persistence;
using Banking.Account.Query.Domain;
using MediatR;

namespace Banking.Account.Query.Application.Feature.BankAccounts.Queries.FindAccountById
{
    public class FindAccountByIdQueryHandler : IRequestHandler<FindAccountById, BankAccount>
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public FindAccountByIdQueryHandler(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<BankAccount> Handle(FindAccountById request, CancellationToken cancellationToken)
        {
            return await _bankAccountRepository.FindByIdentifier(request.Identifier);
        }
    }
}
