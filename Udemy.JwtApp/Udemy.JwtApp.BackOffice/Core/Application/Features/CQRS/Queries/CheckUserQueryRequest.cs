using MediatR;
using Udemy.JwtApp.BackOffice.Core.Application.Dtos;

namespace Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Queries
{
    //bu request geriye CheckUserResponseDto dönecek.
    public class CheckUserQueryRequest : IRequest<CheckUserResponseDto>
    {
        //kullanıcı giriş işlemi yaptığı sırada ben onun girdiği hangi alanlarla db'De bu alanlar uyuyor mu diye bakmalıyım yani check işlemi yapılırken bana hangi alanlar lazım
        //bu alanlar null olamaz 
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}
