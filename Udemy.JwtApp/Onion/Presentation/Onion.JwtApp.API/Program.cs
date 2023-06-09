using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Onion.JwtApp.Application;
using Onion.JwtApp.Application.Tools;
using Onion.JwtApp.Persistence;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Auth işlemleri JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    //https gereksinimi kapatıyorum
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        //JwtTokenSettings classımızdaki değişkenleri burda kullanıcaz
        ValidAudience = JwtTokenDefaults.Audience,
        ValidIssuer = JwtTokenDefaults.Issuer,
        ClockSkew = TimeSpan.Zero,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key)),
        ValidateIssuerSigningKey = true
    };
});


//bunu buraya ekleme sebebeimiz AddPersistenceServicesı DI Conteinera register edebilmek.
//tüm ServiceRegistrationlarımın register işlemlerini burada yapmalıyım.
// ctrl + k + s ile böyle bir region attım karışmasın diye bunun etkisi yok tamamen görüntü.
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

//jwt için bu da koyulmalı.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
