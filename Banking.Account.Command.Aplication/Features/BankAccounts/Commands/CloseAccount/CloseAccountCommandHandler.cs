﻿
using Banking.Account.Command.Aplication.Aggregates;
using Banking.Cqrs.Core.Handlers;
using MediatR;

namespace Banking.Account.Command.Aplication.Features.BankAccounts.Commands.CloseAccount
{
    public class CloseAccountCommandHandler : IRequestHandler<CloseAccountCommand, bool>
    {
        private readonly EventSourcingHandler<AccountAggregate> _eventSourcingHandler;

        public CloseAccountCommandHandler(EventSourcingHandler<AccountAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        public async Task<bool> Handle(CloseAccountCommand request, CancellationToken cancellationToken)
        {
            var aggregate = await _eventSourcingHandler.GetById(request.Id);
            aggregate.CloseAccount();
            await _eventSourcingHandler.Save(aggregate);
            return true;
        }
    }
}
