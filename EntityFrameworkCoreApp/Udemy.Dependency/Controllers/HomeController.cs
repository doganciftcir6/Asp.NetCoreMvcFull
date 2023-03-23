using Microsoft.AspNetCore.Mvc;
using Udemy.Dependency.Services;

namespace Udemy.Dependency.Controllers
{
    public class HomeController : Controller
    {
        //Dependency Injection
        private readonly IProductService _productService;
        private readonly ITransientService _transientService;
        private readonly ISingletonService _singletonService;
        private readonly IScopedService _scopedService;
        public HomeController(IProductService productService, ITransientService transientService, ISingletonService singletonService, IScopedService scopedService)
        {
            _productService = productService;
            _transientService = transientService;
            _singletonService = singletonService;
            _scopedService = scopedService;
        }
        public IActionResult Index()
        {
            //dependency injection
            //var total = _productService.GetTotal();

            ViewBag.Singleton = _singletonService.GuidId;
            ViewBag.Transient = _transientService.GuidId;
            ViewBag.Scoped = _scopedService.GuidId;

            return View();
        }
    }
    //Dependency  Injection
    public interface IProductService
    {
        int GetTotal();
    }
    public class ProductManager : IProductService
    {
        public int GetTotal()
        {
            return 40;
        }
    }
}
