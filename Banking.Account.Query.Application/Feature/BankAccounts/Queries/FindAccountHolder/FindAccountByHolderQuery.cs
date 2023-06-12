

using Banking.Account.Query.Domain;
using MediatR;

namespace Banking.Account.Query.Application.Feature.BankAccounts.Queries.FindAccountHolder
{
    public class FindAccountByHolderQuery: IRequest<IEnumerable<BankAccount>>
    {
        public string AccountHolder { get; set; } = string.Empty;
    }
}
