namespace Udemy.EfCore.Data.Entities
{
    //TABLE PER HİERARCHY 
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    //Bunlarun biri saatlik biri günlük ücret alıyor.
    public class PartTimeEmployee : Employee
    {
        public decimal DailyMage { get; set; }
    }
    public class FullTimeEmployee : Employee
    {
        public decimal HourlyMage { get; set; }
    }
}
