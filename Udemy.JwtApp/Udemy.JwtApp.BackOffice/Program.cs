using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using Udemy.JwtApp.BackOffice.Core.Application.Interfaces;
using Udemy.JwtApp.BackOffice.Core.Application.Mappings;
using Udemy.JwtApp.BackOffice.Infrastructure.Tools;
using Udemy.JwtApp.BackOffice.Persistance.Context;
using Udemy.JwtApp.BackOffice.Persistance.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//contexti baðlayalým
builder.Services.AddDbContext<UdemyJwtContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Local"));
});

//repository'lerin register iþlemi scopes dependency injection ile ele alabilmek için.
//generic repository'in register iþlemi bu þekilde olur.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

//projeye mediator ekleyelim command ve querylerimiz için.
//Assembly içerisinde çalýþtýðýn yeri gel ekle diyebiliriz.
//artýk controller içerisinde IMediator'dan bir örnek alabiliriz dependency injection ile.
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

//Automapper'ý register edelim
//içerisinde ActionDelege geçerek mapper profillerinide register yapalým.
//Bu sayede artýk IMapperdan dependency injection ile bir map örneði ele alabilirim.
builder.Services.AddAutoMapper(opt =>
{
    opt.AddProfiles(new List<Profile>()
    {
        new ProductProfile(),
        new CategoryProfile()
    });
});

//jwt için corse CONFÝGURESÝ
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("GlobalCors", config =>
    {
        config.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

//token'ýn register ve configurationlarý
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    //https gereksinimi kapatýyorum
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        //JwtTokenSettings classýmýzdaki deðiþkenleri burda kullanýcaz
        ValidAudience = JwtTokenSettings.Audience,
        ValidIssuer = JwtTokenSettings.Issuer,
        ClockSkew = TimeSpan.Zero,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenSettings.Key)),
        ValidateIssuerSigningKey = true
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//jwt için corse registeri
app.UseCors("GlobalCors");

//token için
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
