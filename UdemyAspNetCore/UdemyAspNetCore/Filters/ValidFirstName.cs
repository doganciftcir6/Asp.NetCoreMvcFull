using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using UdemyAspNetCore.Models;

namespace UdemyAspNetCore.Filters
{
    public class ValidFirstName : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //
            var dictionary = context.ActionArguments.FirstOrDefault(i => i.Key == "customer");
            var customer = dictionary.Value as Customer;
            //eğer action metotunun parametre içine gelen bilginin firstname alanı yavuza eşitese bu url ye git diyebiliriz action metotu çalışmadan önce oluyor bu
            if (customer.FirstName == "yavuz")
            { 
            context.Result = new RedirectResult("/Home/Index");
            base.OnActionExecuting(context);
            }
        }
    }
}
