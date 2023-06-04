
using Banking.Account.Command.Aplication.Aggregates;
using Banking.Cqrs.Core.Handlers;
using MediatR;

namespace Banking.Account.Command.Aplication.Features.BankAccounts.Commands.WithdrawnFund
{
    public class WithdrawFundsCommandHandler : IRequestHandler<WithdrawnFundCommand, bool>
    {
        private readonly EventSourcingHandler<AccountAggregate> _eventSourcingHandler;

        public WithdrawFundsCommandHandler(EventSourcingHandler<AccountAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        public async Task<bool> Handle(WithdrawnFundCommand request, CancellationToken cancellationToken)
        {
            var aggregate = await _eventSourcingHandler.GetById(request.Id);
            aggregate.WithdrawFunds(request.Amount);
            await _eventSourcingHandler.Save(aggregate);
            return true;
        }

    }
}
