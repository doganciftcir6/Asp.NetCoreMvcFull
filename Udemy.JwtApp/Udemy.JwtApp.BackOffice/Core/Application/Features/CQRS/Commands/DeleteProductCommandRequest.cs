using MediatR;

namespace Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Commands
{
    //requestimde geriye bir şey dönmediğimi görebiliriz IRequest'in yanı boş.
    public class DeleteProductCommandRequest : IRequest
    {
        //bemim burdan beklediğim şey bir id değeri bu id değeriyle ilgili product'A ulaşıcam sonra silicem.
        public int Id { get; set; }
        //dışarıdan gelen ıd değerini burda prop olarak bulunan ıd nin içine at diyoruz.
        public DeleteProductCommandRequest(int id)
        {
            this.Id = id;
        }
    }
}
