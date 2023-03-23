using Udemy.BankApp.Web.Data.Entities;

namespace Udemy.BankApp.Web.Data.Interfaces
{
    public interface IAccountRepository
    {
        //Db'ye Account ekleme metotu
        public void Create(Account account);
    }
}
