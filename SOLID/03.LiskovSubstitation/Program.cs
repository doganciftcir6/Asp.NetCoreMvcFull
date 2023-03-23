using System;

namespace _03.LiskovSubstitation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //LİSKOV BİZE BUNU YAPABİLİRSİN DİYOR.
            Employee employee = new PartTimeEmployee();
        }
    }
    //Bir part time ve full time çalışacak çalışanımız olsun.
    //bu çalışan eğer part time ise maaşı günlük hesaplansın.
    //Bu çalışan eğer full time ise maaşı saatlik hesaplansın.
    //elimizde 2 tipte ücret alan personel söz konusu bu noktada bunu duruma göre parçalamamız lazım
    class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    class PartTimeEmployee : Employee
    {
        public decimal DailyPrice { get; set; }
    }
    class FullTimeEmployee : Employee
    {
        public decimal HourlyPrice { get; set; }
    }
}
