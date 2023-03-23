using System.Collections.Generic;
using System.Linq;
using Udemy.BankApp.Web.Data.Context;
using Udemy.BankApp.Web.Data.Entities;
using Udemy.BankApp.Web.Data.Interfaces;

namespace Udemy.BankApp.Web.Data.Repositories
{
    //User ile ilgili tekrar edecek olan işlerimi burada gerçekleştiririm.
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        //dependency injection ile veritabanını ele alalım
        private readonly BankContext _context;

        public ApplicationUserRepository(BankContext context)
        {
            _context = context;
        }

        //tüm kayıtları getiren metot

        public List<ApplicationUser> GetAll()
        {
            return _context.ApplicationUsers.ToList();
        }

        //sadece ilgili ıd'deki kayıtı getiren metot
        public ApplicationUser GetById(int id)
        {
            return _context.ApplicationUsers.SingleOrDefault(x => x.Id == id);
        }
    }
}
