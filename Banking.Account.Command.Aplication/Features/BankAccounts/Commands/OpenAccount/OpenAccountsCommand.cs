
using MediatR;

namespace Banking.Account.Command.Aplication.Features.BankAccounts.Commands.OpenAccount
{
    public class OpenAccountsCommand: IRequest<bool>
    {
        public string Id { get; set; } = string.Empty;

        public string AccountHolder { get; set; } = string.Empty;

        public string AccountType { get; set; } = string.Empty;

        public double OpeningBalance { get; set; }
    }
}
