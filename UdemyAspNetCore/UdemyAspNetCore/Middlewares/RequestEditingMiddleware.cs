using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace UdemyAspNetCore.Middlewares
{
    public class RequestEditingMiddleware
    { 
        //dependiy injection
        private RequestDelegate _requestDelegate;
        public RequestEditingMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task Invoke(HttpContext context)
        {
            //ysk.com.tr/yavuz  => /yavuz => Path
            if (context.Request.Path.ToString() == "/yavuz")
             await context.Response.WriteAsync("hosgeldin yavuz");
            else
            {
                //Server tarafına ilerleten
                await _requestDelegate.Invoke(context);
            }
         
        }
    }
}
