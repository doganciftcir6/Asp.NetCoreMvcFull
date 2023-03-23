using MediatR;

namespace Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Commands
{
    //requestimde geriye bir şey dönmediğimi görebiliriz IRequest'in yanı boş.
    public class CreateCategoryCommandRequest : IRequest
    {
        //create işlemi yaparken bana hangi alanlar lazımdır?
        public string? Definition { get; set; }
    }
}
