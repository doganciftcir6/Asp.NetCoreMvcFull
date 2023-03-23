using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

//mvc
builder.Services.AddControllersWithViews();

//api taraf�yla haberle�ebilmek i�in , kanstrakt�r arac�l���yla ilgili endpointleri vs kullanabilmek i�in, IHttpClientFactory'� dependency injection olarak ele alabilmek i�in.
builder.Services.AddHttpClient();

//apideki token taraf�n� frontendde i�lemek cookie configurasyonu.
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

//Identity i�lemleri
app.UseAuthentication();
app.UseAuthorization();

//default route
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
