
using MediatR;

namespace Banking.Account.Command.Aplication.Features.BankAccounts.Commands.WithdrawnFund
{
    public class WithdrawnFundCommand: IRequest<bool>
    {
        public string Id { get; set; } =  string.Empty;

        public double Amount { get; set; }
    }
}
