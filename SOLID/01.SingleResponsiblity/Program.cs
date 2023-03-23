using System;
using System.ComponentModel.DataAnnotations;

namespace _01.SingleResponsiblity
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
    //SOLIDIN S SİNİ ÇİĞNEMİŞ OLDUK YANLIŞ BİR KULLANIM OLDU
    class Customer
    {
        [Required]
        public string Name { get; set; } 
        public bool ValidateName (string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }
        public void SayHello(string name)
        {
            Console.WriteLine("hELLO" + name);
        }
    }
    //YAPMAMIZ GEREKEN İSE BU ŞEKİLDE OLMALI VALİDATİON İŞLEMLERİ AYRI BİR CLASSTA VE HELLO DİYE SÖYLEME İŞİ AYRI BİR CLASSTA OLMALI
    class CustomerValidator
    {
        [Required]
        public string Name { get; set; }
        public bool ValidateName(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }
    }
    class CustomerManager
    {
        public void SayHello(string name)
        {
            Console.WriteLine("hELLO" + name);
        }

    }
}

