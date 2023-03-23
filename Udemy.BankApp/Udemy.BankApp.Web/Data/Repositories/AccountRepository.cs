using Udemy.BankApp.Web.Data.Context;
using Udemy.BankApp.Web.Data.Entities;
using Udemy.BankApp.Web.Data.Interfaces;

namespace Udemy.BankApp.Web.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        //dependency injection
        private readonly BankContext _context;

        public AccountRepository(BankContext context)
        {
            _context =context;
        }
        //Db'ye Account ekleme metotu
        public void Create(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }
    }
}
