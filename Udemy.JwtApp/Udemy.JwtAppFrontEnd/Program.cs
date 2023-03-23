using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

//mvc
builder.Services.AddControllersWithViews();

//api tarafýyla haberleþebilmek için , kanstraktýr aracýlýðýyla ilgili endpointleri vs kullanabilmek için, IHttpClientFactory'ý dependency injection olarak ele alabilmek için.
builder.Services.AddHttpClient();

//apideki token tarafýný frontendde iþlemek cookie configurasyonu.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "/Account/SignIn";
    opt.LogoutPath = "/Account/Logout";
    opt.AccessDeniedPath = "/Account/AccessDenied";
    opt.Cookie.SameSite = SameSiteMode.Strict;
    opt.Cookie.HttpOnly = true;
    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    opt.Cookie.Name = "UdemyJwtCookie";
});


var app = builder.Build();

//wwwroot
app.UseStaticFiles();

app.UseRouting();

//Identity iþlemleri
app.UseAuthentication();
app.UseAuthorization();

//default route
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
