using System.Collections.Generic;
using System.Linq;
using Udemy.BankApp.Web.Data.Entities;
using Udemy.BankApp.Web.Models;

namespace Udemy.BankApp.Web.Mapping
{
    // Bu class içerisinde bir takım metotlar yazıcaz. Bize entity olarak bir liste gelecek gelen listeyi modele çeviricez.
    public class UserMapper : IUserMapper
    {
        //gelen ApplicationUser Entity'sini Modele dönüştürelim metotta.
        public List<UserListModel> MapToListOfUserList(List<ApplicationUser> users)
        {
            return users.Select(x => new UserListModel
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname
            }).ToList();
        }
        //aynı işin ıd'ye göre kayıt getiren metotu içinde yapalım.
        public UserListModel MapToUserList(ApplicationUser user)
        {
            return new UserListModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname
            };
        }
    }
}
