using MediatR;

namespace Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Commands
{
    //requestimde geriye bir şey dönmediğimi görebiliriz IRequest'in yanı boş.
    public class UpdateProductCommandRequest : IRequest
    {
        //Bize update işlemi yaparken hangi alanlar lazım?
        //burada id yi api tarafındaki route tan almayacağım için ctor oluşturmadım id için.
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
