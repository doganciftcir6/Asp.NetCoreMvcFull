using Onion.JwtApp.Application;
using Onion.JwtApp.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.UseAuthorization();

app.MapControllers();

app.Run();
