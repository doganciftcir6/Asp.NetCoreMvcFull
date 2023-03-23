using System.Collections.Generic;
using Udemy.BankApp.Web.Data.Entities;
using Udemy.BankApp.Web.Models;

namespace Udemy.BankApp.Web.Mapping
{
    // Bu class içerisinde bir takım metotlar yazıcaz. Bize entity olarak bir liste gelecek gelen listeyi modele çeviricez.
    public interface IUserMapper
    {
        //gelen ApplicationUser Entity'sini Modele dönüştürelim metotta.
        List<UserListModel> MapToListOfUserList(List<ApplicationUser> users);
        //aynı işin ıd'ye göre kayıt getiren metotu içinde yapalım.
        UserListModel MapToUserList(ApplicationUser user);
    }
}
