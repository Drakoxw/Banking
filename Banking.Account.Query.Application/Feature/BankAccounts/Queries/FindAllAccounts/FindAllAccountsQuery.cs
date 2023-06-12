
using Banking.Account.Query.Domain;
using MediatR;

namespace Banking.Account.Query.Application.Feature.BankAccounts.Queries.FindAllAccounts
{
    public class FindAllAccountsQuery: IRequest<IEnumerable<BankAccount>>
    {

    }
}
