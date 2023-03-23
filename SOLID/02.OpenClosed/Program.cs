using System;

namespace _02.OpenClosed
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SalaryCalculator calculator = new SalaryCalculator(new FullTimeEmployee());
            calculator.CaltulateSalary();
            SalaryCalculator calculator2 = new SalaryCalculator(new PartTimeEmployee());
            calculator2.CaltulateSalary();
            SalaryCalculator calculator3 = new SalaryCalculator(new SupportEmployee());
            calculator3.CaltulateSalary();
            Console.WriteLine("Hello World!");
        }
    }
    //düşünün ki bir çalışanımız var ve biz bunun part time ya da full time olma durumuna göre maaşını hesaplıcaz.
    //bir süre sonra firmamda yoğunluk arttı dışarıdan  2 günlüğüne çalışacak destek personelleride alıcam. Bunların maaşlarınıda farklı hesaplıcam. Bu durumda hesap yaptığım son classım değişim gösterecektir. Bu şekilde yeni bir durum eklediğinizde iş yapan classımızı değiştiriyorsanız OpenClosed'ı çiğnediniz. Böyle bir durumda bir interfaceden faydalanabiliriz.
    interface IEmployee
    {
        void CalculateSalary();
    }
    class FullTimeEmployee : IEmployee
    {
        public void CalculateSalary()
        {
            Console.WriteLine("Full");
        }
    }
    class PartTimeEmployee : IEmployee
    {
        public void CalculateSalary()
        {
            Console.WriteLine("Part");
        }
    }
    class SupportEmployee : IEmployee
    {
        public void CalculateSalary()
        {
            Console.WriteLine("Support");
        }
    }
    class SalaryCalculator
    {
        //public void CalculateSalary(string employyeType)
        //{
        //    if(employyeType == "Full")
        //    {
        //        new FullTimeEmployee().CalculateSalary();
        //    }
        //    else if(employyeType == "Support")
        //    {
        //        new SupportEmployee().CalculateSalary();
        //    }
        //    else
        //    {
        //        new PartTimeEmployee().CalculateSalary();
        //    }

        //}

        private readonly IEmployee employee;
        //kanstraktır
        public SalaryCalculator(IEmployee employee)
        {
            this.employee = employee;
        }
        public void CaltulateSalary()
        {
            this.employee.CalculateSalary();
        }
    }
}
