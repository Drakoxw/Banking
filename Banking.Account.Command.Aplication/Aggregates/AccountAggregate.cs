
using Banking.Account.Command.Aplication.Features.BankAccounts.Commands.OpenAccount;
using Banking.Cqrs.Core.Domain;
using Banking.Cqrs.Core.Events;

namespace Banking.Account.Command.Aplication.Aggregates
{
    public class AccountAggregate: AggregateRoot
    {
        public bool Active { get; set; }

        public double Balance { get; set; }

        public AccountAggregate() { }


        public AccountAggregate(OpenAccountsCommand command) 
        {
            var accountOpenendEvent = new AccountOpenedEvent(
                command.Id,
                command.AccountHolder,
                command.AccountType,
                DateTime.Now,
                command.OpeningBalance
            );

            RaiseEvent(accountOpenendEvent);
        }

        public void Apply(AccountOpenedEvent @event)
        {
            Id = @event.Id;
            Active = true;
            Balance = @event.OpeningBalance;
        }

        public void DepositFunds(double amount)
        {
            if (!Active)
            {
                throw new Exception("No se puede depositar a una cuenta cancelada!");
            }

            if (amount <= 0)
            {
                throw new Exception("El deposito debe ser mayor a cero!");
            }

            var fundsDepositEvent = new FundsDepositedEvent(Id)
            {
                Id = Id,
                Amount = amount,
            };

            RaiseEvent(fundsDepositEvent);
        }

        public void Apply(FundsDepositedEvent @event)
        {
            Id = @event.Id;
            Balance += @event.Amount;
        }

        public void WithdrawFunds(double amount)
        {
            if (!Active)
            {
                throw new Exception("No se puede retirar de una cuenta cancelada!");
            }

            var fundsWithdrawEvent = new FundsWithdrawnEvent(Id)
            {
                Id = Id,
                Amount = amount
            };

            RaiseEvent(fundsWithdrawEvent);
        }

        public void Apply(FundsWithdrawnEvent @event)
        {
            Id = @event.Id;
            Balance -= @event.Amount;
        }

        public void CloseAccount()
        {
            if (!Active)
            {
                throw new Exception("La cuenta esta cerrada!");
            }

            var accountCloseEvent = new AccountCloseEvent(Id);

            RaiseEvent(accountCloseEvent);
        }

        public void Apply(AccountCloseEvent @event)
        {
            Id = @event.Id;
            Active = false;
        }

    }
}
