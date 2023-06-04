
using MediatR;

namespace Banking.Account.Command.Aplication.Features.BankAccounts.Commands.DepositeFund
{
    internal class DepositeFundCommand: IRequest<bool>
    {
        public string Id { get; set; } = string.Empty;

        public double Amount { get; set; }
    }
}
