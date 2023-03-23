using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace UdemyAspNetCore.Middlewares
{
    public class ResponseEditingMiddleware
    {
        private RequestDelegate _requestDelegate;
        public ResponseEditingMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task Invoke(HttpContext context) 
        {
            //Responsun client'a gitmesi için veya bir başka middlewear'a geçmesi için Invoke metotunu kullanmam lazım
          await _requestDelegate.Invoke(context);
            //artık responsu ele alabilirim
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
             await context.Response.WriteAsync("Sayfa Bulunamadı.");
        }

    }
}
