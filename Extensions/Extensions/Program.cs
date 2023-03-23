using System;

namespace Extensions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Matematik mat = new Matematik();
            mat.Topla(3,5);
            //görüldüğü üzre ana classım üzerinden ek classtaki metota ulaşabildim.
            mat.Carp(2, 3);


            Console.WriteLine("Hello World!");
        }
    }
    //bu class sadece toplama işlemi yapıyorsa ben istiyorum ki bu class'a ek özellikler tanımlayayım ve bu özellikleri bu classa dokunmadan yapayım.
    //Siz bir Extension Metot yazıyosanız bu metotu yazdığınız class’ın static olması gerekiyor.  
    class Matematik
    {
        public int Topla(int sayi1, int sayi2)
        {
            return sayi1 + sayi2;
        }
    }
    public static class MatExtension
    {
        public static int Carp(this Matematik matematik, int sayi1, int sayi2)
        {
            return sayi1 * sayi2;
        }
    }
}
