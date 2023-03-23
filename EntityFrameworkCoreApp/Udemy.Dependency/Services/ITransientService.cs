namespace Udemy.Dependency.Services
{
    public interface ServiceBase
    {
        string GuidId { get; }
    }
    //DRY (hepsinde aynı metotun bulunması tekrara düşmesi)
    public interface ITransientService : ServiceBase
    {
        //string GuidId { get; }
    }
    public interface IScopedService : ServiceBase
    {
        //string GuidId { get; }
    }
    public interface ISingletonService : ServiceBase
    {
        //string GuidId { get; }
    }

}
