using MediatR;

namespace Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Commands
{
    //requestimde geriye bir şey dönmediğimi görebiliriz IRequest'in yanı boş.
    public class UpdateCategoryCommandRequest : IRequest
    {
        //Bize update işlemi yaparken hangi alanlar lazım?
        //burada id yi api tarafındaki route tan almayacağım için ctor oluşturmadım id için.
        public int Id { get; set; }
        public string? Definition { get; set; }
    }
}
