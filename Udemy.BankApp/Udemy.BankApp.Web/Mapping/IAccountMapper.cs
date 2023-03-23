using Udemy.BankApp.Web.Data.Entities;
using Udemy.BankApp.Web.Models;

namespace Udemy.BankApp.Web.Mapping
{
    //Db'ye ekleme yaparken modeli Entity'e çevirmem gerekiyor db entity'den anlar. Bunun için mapper kullanıcaz.
    public interface IAccountMapper
    {
        Account Map(AccountCreateModel model);
    }
}
