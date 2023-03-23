using System;
using System.Collections.Generic;

namespace _04.PredicateDelege
{
    internal class Program
    {
        static void Main(string[] args)
        {
         Predicate<string> predicate = new Predicate<string>(YavuzuBul);
         var gelenData = predicate.Invoke("Yavuz");

            List<string> isimler = new List<string>() { "Yavuz", "Ahmet", "Ayşe", "Tuğba", "Serkan", "Fatih" };
           var bulunanIsim = isimler.Find(predicate);
        }

        private static bool YavuzuBul(string obj)
        {
            return obj == "Yavuz";
        }
    }
}
