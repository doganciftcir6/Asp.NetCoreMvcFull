using Udemy.BankApp.Web.Data.Entities;
using Udemy.BankApp.Web.Models;

namespace Udemy.BankApp.Web.Mapping
{
    //Db'ye ekleme yaparken modeli Entity'e çevirmem gerekiyor db entity'den anlar. Bunun için mapper kullanıcaz.
    public class AccountMapper : IAccountMapper
    {
        public Account Map(AccountCreateModel model)
        {
            return new Account()
            {
                AccountNumber = model.AccountNumber,
                ApplicationUserId = model.AccountNumber,
                Balance = model.Balance
            };
        }
    }
}
