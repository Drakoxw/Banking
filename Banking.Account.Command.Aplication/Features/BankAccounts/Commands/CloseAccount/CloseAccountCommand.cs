
using MediatR;

namespace Banking.Account.Command.Aplication.Features.BankAccounts.Commands.CloseAccount
{
    public class CloseAccountCommand: IRequest<bool>
    {
        public string Id { get; set; } = string.Empty;

        public CloseAccountCommand() { }
    }
}
