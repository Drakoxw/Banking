
using Banking.Account.Query.Application.Contracts.Persistence;
using Banking.Account.Query.Domain;
using Banking.Account.Query.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Banking.Account.Query.Infrastructure.Repositories
{
    public class BankAccountRepository : RepositoryBase<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(MySqlDbContext context) : base(context)
        {
        }

        public async Task DeleteByIdentifier(string identifier)
        {
            var bankAccount = await _context.BankAccount!.Where(x => x.Identifier == identifier).FirstOrDefaultAsync();

            if (bankAccount == null) 
            {
                throw new Exception($"No se pudo eliminar la cuenta con el identificador: {identifier}");
            }

            _context.BankAccount!.Remove(bankAccount);
            await _context.SaveChangesAsync();
        }

        public async Task DepositeBankAccountByIdentifier(BankAccount bankAccount)
        {
            var account = await _context.BankAccount!.Where(x => x.Identifier == bankAccount.Identifier).FirstOrDefaultAsync();

            if (account == null)
            {
                throw new Exception($"No se pudo actualizar la cuenta con el identificador: {bankAccount.Identifier}");
            }

            account.Balance += bankAccount.Balance;

            await UpdateAsync(account);
        }

        public async Task<IEnumerable<BankAccount>> FindByAccountHolder(string accountHolder)
        {
            return await _context.BankAccount!.Where(x => x.AccountHolder == accountHolder).ToListAsync();
        }

        public async Task<IEnumerable<BankAccount>> FindByBalanceGreaterThan(double balance)
        {
            return await _context.BankAccount!.Where(x => x.Balance >= balance).ToListAsync();
        }

        public async Task<IEnumerable<BankAccount>> FindByBalanceLessThan(double balance)
        {
            return await _context.BankAccount!.Where(x => x.Balance <= balance).ToListAsync();
        }

        public async Task<BankAccount> FindByIdentifier(string identifier)
        {
            return await _context.BankAccount!.Where(x => x.Identifier == identifier).FirstOrDefaultAsync();
        }

        public async Task WithdrawnBankAccountByIdentifier(BankAccount bankAccount)
        {
            var account = await _context.BankAccount!.Where(x => x.Identifier == bankAccount.Identifier).FirstOrDefaultAsync();

            if (account == null)
            {
                throw new Exception($"No se pudo actualizar la cuenta con el identificador: {bankAccount.Identifier}");
            }

            if (account.Balance < bankAccount.Balance)
            {
                throw new Exception("Fondos insuficientes");
            }

            account.Balance -= bankAccount.Balance;

            await UpdateAsync(account);
        }
    }
}
