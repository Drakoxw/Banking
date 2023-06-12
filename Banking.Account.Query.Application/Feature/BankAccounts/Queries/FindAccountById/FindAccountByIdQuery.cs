
using Banking.Account.Query.Domain;
using MediatR;

namespace Banking.Account.Query.Application.Feature.BankAccounts.Queries.FindAccountById
{
    public class FindAccountById: IRequest<BankAccount>
    {
        public string Identifier { get; set; } = string.Empty;
    }
}
