using System;

namespace _05.DependencyInversion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //bu action sayfalarına tek tek istek attığımı düşünelim
            HomeIndex();
            CategoryIndex();
            CategoryIndex2();
            CategoryIndex3();

        }
        //düşünki benim bir sürü controller'ım var ve bu controllerın içerisindeki action metotlarının altına geldim şöyle şeyler yazdım bunları action metotlar olarak düşünelim
        static void HomeIndex()
        {
            //CategoryRepositoryEf ef = new CategoryRepositoryEf();
            //ef.GetCategories();

            //seneryomuz şöyle değişecek
            Container container = new Container();
            container.GetInstance().GetCategories();
        }
        static void CategoryIndex()
        {
            //CategoryRepositoryEf ef = new CategoryRepositoryEf();
            //ef.GetCategories();

            Container container = new Container();
            container.GetInstance().GetCategories();
        }
        static void CategoryIndex2()
        {
            //CategoryRepositoryEf ef = new CategoryRepositoryEf();
            //ef.GetCategories();

            Container container = new Container();
            container.GetInstance().GetCategories();
        }
        static void CategoryIndex3()
        {
            //CategoryRepositoryEf ef = new CategoryRepositoryEf();
            //ef.GetCategories();

            Container container = new Container();
            container.GetInstance().GetCategories();
        }
    }
    //örnekleme işlemini container'ın içinde gerçekleştiricem. Yani dicem ki sen ICategoryRepository gibi bir şey görürsen eğer gel bunu CategoryRepositoryEf olarak al. Yani bunun örneğini bana ver.
    class Container
    {
        public ICategoryRepository GetInstance()
        {
            //artık ileride bir ORM değişikliği yaptığım zaman sadece burada değişikliği yapıcam tek bir yerden değişiklik yaparak tüm proje içinde dğeişiklik yapmış olacağım.
            return new CategoryRepositoryEf();
            //return new CategoryRepositoryDp();
        }

    }
    interface ICategoryRepository 
    {
        void GetCategories();
    }
    //Bir classımız var bu classımız EntityFrameWork gibi bir ORM kullanarak gidiyor veritabanıyla haberleşiyor bana bir veri getiriyor.

    //bu class category tablosuyla ilgili işlemleri yapıyor. Categoryleri GetCategories metotuyla bana getiriyor.
    class CategoryRepositoryEf : ICategoryRepository
    {
        //DEPENDECNY INJECTION
        //•	Readonly dersek eğer bir şey sadece kanstraktır içinde setlenebilir. Bunu başka bir yerde setleyemeyiz.
        //•	Dependency Injection yapısı gelir container’da belirtmiş olduğnuz şeye göre ilgili örneği alır fırlatır. Siz bu örneği toplayıcı bir interface’nin üzerinde taşıyabilirisinz. Mimari böyle çalışıyor. Arkasındaki mantık Depndency Inversion’da belirtmiş olduğumuz container mantığı aslında.
        private readonly ICategoryRepository _repository;
        public CategoryRepositoryEf(ICategoryRepository _repository)
        {

        }
        public void GetCategories()
        {
            Console.WriteLine("Ef");
        }
    }
    //daha sonra benim ihtiyacım değişti diyelim ORM olarka artık EF değilde dapper kullanıcam böyle bir durumda aciton metotlarımın içinde örnekleme yaptığım durumları tek tek buna uygun olarak değiştirmek zorunda kalıyorum. Çünkü ben EF 'ye sıkı sıkıya bağlandım. Neden bağlıyım new kullandığım için. New kullanırsak bu şekilde bağımlı oluruz. New'den kurtulmamız gerekiyor bu yüzden dependencyInversion şunu söylüyor. Sen bu bağımlılığından kurtul. Bir interface örneklenemezdi ama bir classın örneğini üzerinde taşıyabilirdi. İnterface ile çözeriz bu durumu.
    class CategoryRepositoryDp
    {
        public void GetCategories()
        {
            Console.WriteLine("Dp");
        }
    }

}
