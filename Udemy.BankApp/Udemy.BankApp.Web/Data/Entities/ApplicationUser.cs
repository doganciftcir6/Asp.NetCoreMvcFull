using System.Collections.Generic;

namespace Udemy.BankApp.Web.Data.Entities
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        //tablo ilişkisi
        //bir ApplicationUser'ın birden fazla Account'u olabilir.
        public List<Account> Accounts { get; set; }
    }
}
