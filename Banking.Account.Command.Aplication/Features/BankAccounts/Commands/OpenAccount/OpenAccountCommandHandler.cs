
using Banking.Account.Command.Aplication.Aggregates;
using Banking.Cqrs.Core.Handlers;
using MediatR;

namespace Banking.Account.Command.Aplication.Features.BankAccounts.Commands.OpenAccount
{
    public class OpenAccountCommandHandler : IRequestHandler<OpenAccountsCommand, bool>
    {
        private readonly EventSourcingHandler<AccountAggregate> _eventSourcingHandler;

        public OpenAccountCommandHandler(EventSourcingHandler<AccountAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        public async Task<bool> Handle(OpenAccountsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var aggregate = new AccountAggregate(request);
                await _eventSourcingHandler.Save(aggregate);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
