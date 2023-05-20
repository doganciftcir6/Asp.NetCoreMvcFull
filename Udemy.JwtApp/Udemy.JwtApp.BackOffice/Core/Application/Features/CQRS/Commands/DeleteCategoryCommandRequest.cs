using MediatR;

namespace Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Commands
{
    //requestimde geriye bir şey dönmediğimi görebiliriz IRequest'in yanı boş.
    public class DeleteCategoryCommandRequest : IRequest
    {
        //bemim burdan beklediğim şey bir id değeri bu id değeriyle ilgili category'e ulaşıcam sonra silicem.
        public int Id { get; set; }
        //dışarıdan gelen ıd değerini burda prop olarak bulunan ıd nin içine at diyoruz.
        //burada bu id yi api tarafında route tan alacağım için ctor oluşturuyorum.
        //route tan girilen ıd parametre içindeki küçük ıd ye düşer ve bu classın prop Id sine setlenir.
        public DeleteCategoryCommandRequest(int id)
        {
            this.Id = id;
        }
    }
}
