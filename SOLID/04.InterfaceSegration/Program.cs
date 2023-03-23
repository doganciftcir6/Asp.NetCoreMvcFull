using System;

namespace _04.InterfaceSegration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
    //database ile alışveriş yapan classların metotlarını barındıracak bu interface.
    //BU KULLANIM YANLIŞ OLACAKTIR 
    public interface IRepository
    {
        void Add();
        void GetCategories();
        void GetProducts();
    }
    //YANLIŞ KULLANIM YAPMAMAK İÇİN ŞUNU YAPMALIYIZ
    public interface ICategoryRepository
    {
        void GetCategories2();
    }
    public interface IProductRepository
    {
        void GetProducts2();
    }
    //category'le alakalı işleri yapan classım var.
    //BU KULLANIM YANLIŞ OLACAKTIR ÇÜNKÜ CATEGORY CLASSININ İÇİNDE PRODUCT İLE İLGİLİ İŞLEMLER OLMAMALI
    public class CategoryRepository : IRepository //(ICategoryRepository) 'den kalıtmamız lazım
    {
        public void Add()
        {
            Console.WriteLine("Added.");
        }

        public void GetCategories()
        {
            Console.WriteLine("list category");
        }
        //bu metot burda olmamalı
        public void GetProducts()
        {
            throw new NotImplementedException();
        }
    }
    //product'la alakalı işleri yapan classım var.
    public class ProductRepository : IRepository //(IProductRepository) 'den kalıtmamız lazım
    {
        public void Add()
        {
            Console.WriteLine("Added.");
        }

        //bu metot burda olmamalı
        public void GetCategories()
        {
            throw new NotImplementedException();
        }

        public void GetProducts()
        {
            Console.WriteLine("List products.");
        }
    }
}
