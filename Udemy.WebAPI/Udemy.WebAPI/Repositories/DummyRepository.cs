using Udemy.WebAPI.Interfaces;

namespace Udemy.WebAPI.Repositories
{
    public class DummyRepository : IDummyRepository
    {
        public string GetName()
        {
            return "Yavuz";
        }
    }
}
