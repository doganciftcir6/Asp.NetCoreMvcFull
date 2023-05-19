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

//contexti ba�layal�m
builder.Services.AddDbContext<UdemyJwtContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Local"));
});

//repository'lerin register i�lemi scopes dependency injection ile ele alabilmek i�in.
//generic repository'in register i�lemi bu �ekilde olur.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

//projeye mediator ekleyelim command ve querylerimiz i�in.
//Assembly i�erisinde �al��t���n yeri gel ekle diyebiliriz.
//art�k controller i�erisinde IMediator'dan bir �rnek alabiliriz dependency injection ile.
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

//Automapper'� register edelim
//i�erisinde ActionDelege ge�erek mapper profillerinide register yapal�m.
//Bu sayede art�k IMapperdan dependency injection ile bir map �rne�i ele alabilirim.
builder.Services.AddAutoMapper(opt =>
{
    opt.AddProfiles(new List<Profile>()
    {
        new ProductProfile(),
        new CategoryProfile()
    });
});

//jwt i�in corse CONF�GURES�
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("GlobalCors", config =>
    {
        config.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

//token'�n register ve configurationlar�
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    //https gereksinimi kapat�yorum
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        //JwtTokenSettings class�m�zdaki de�i�kenleri burda kullan�caz
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

//jwt i�in corse registeri
app.UseCors("GlobalCors");

//token i�in
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
