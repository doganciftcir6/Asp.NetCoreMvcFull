using MediatR;

namespace Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Commands
{
    public class RegisterUserCommandRequest : IRequest
    {
        //ben user'ı register ederken hangi alanlara ihtiyacım var?
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
