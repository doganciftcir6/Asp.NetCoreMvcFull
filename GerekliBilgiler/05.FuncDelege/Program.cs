using System;
using System.Collections.Generic;
using System.Linq;

namespace _05.FuncDelege
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> isimler = new() { "Yavuz", "Ahmet", "Berk" };
            //string parametre alsın geriye bool değer dönsün diyebiliriz.
            Func<string,bool> func = new(AyiBul);
            //Where fonksiyonu bizden bir func delegate istiyor.
            var aIcerenIsımler = isimler.Where(func);
            Console.WriteLine(aIcerenIsımler);

        }
            static bool AyiBul(string arg) 
            {
                return arg.Contains("a");
            }
        }
    }

