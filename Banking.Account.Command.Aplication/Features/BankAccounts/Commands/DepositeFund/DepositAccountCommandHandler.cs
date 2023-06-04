
using Banking.Account.Command.Aplication.Aggregates;
using Banking.Cqrs.Core.Handlers;
using MediatR;

namespace Banking.Account.Command.Aplication.Features.BankAccounts.Commands.DepositeFund
{
    public class DepositAccountCommandHandler : IRequestHandler<DepositeFundCommand, bool>
    {
        private readonly EventSourcingHandler<AccountAggregate> _eventSourcingHandler;

        public DepositAccountCommandHandler(EventSourcingHandler<AccountAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        public async Task<bool> Handle(DepositeFundCommand request,
                                       CancellationToken cancellationToken)
        {
            var aggregate = await _eventSourcingHandler.GetById(request.Id);
            aggregate.DepositFunds(request.Amount);
            await _eventSourcingHandler.Save(aggregate);
            return true;
        }
    }
}
