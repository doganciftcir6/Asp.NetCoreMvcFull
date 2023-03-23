namespace Udemy.BankApp.Web.Data.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public int AccountNumber { get; set; }
        //tablo ilişkisi
        //bir ApplicationUser'ın birden fazla Account'u olabilir.
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
