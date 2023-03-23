using System.Collections.Generic;
using Udemy.BankApp.Web.Data.Entities;

namespace Udemy.BankApp.Web.Data.Interfaces
{
    //User ile ilgili tekrar edecek olan işlerimi burada gerçekleştiririm.
    public interface IApplicationUserRepository
    {
        //tüm kayıtları getiren metot
        public List<ApplicationUser> GetAll();
        //sadece ilgili ıd'deki kayıtı getiren metot
        public ApplicationUser GetById(int id);
    }
}
