using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Onion.JwtApp.Application;
using Onion.JwtApp.Application.Tools;
using Onion.JwtApp.Persistence;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Auth i�lemleri JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    //https gereksinimi kapat�yorum
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        //JwtTokenSettings class�m�zdaki de�i�kenleri burda kullan�caz
        ValidAudience = JwtTokenDefaults.Audience,
        ValidIssuer = JwtTokenDefaults.Issuer,
        ClockSkew = TimeSpan.Zero,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key)),
        ValidateIssuerSigningKey = true
    };
});


//bunu buraya ekleme sebebeimiz AddPersistenceServices� DI Conteinera register edebilmek.
//t�m ServiceRegistrationlar�m�n register i�lemlerini burada yapmal�y�m.
// ctrl + k + s ile b�yle bir region att�m kar��mas�n diye bunun etkisi yok tamamen g�r�nt�.
#region Service Registration

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration); 

#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//jwt i�in bu da koyulmal�.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
