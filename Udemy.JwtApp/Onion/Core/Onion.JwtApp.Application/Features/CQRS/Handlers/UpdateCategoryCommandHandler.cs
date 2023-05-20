using MediatR;
using Onion.JwtApp.Application.Features.CQRS.Commands;
using Onion.JwtApp.Application.Interfaces;
using Onion.JwtApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.JwtApp.Application.Features.CQRS.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest>
    {
        private readonly IRepository<Category> _repository;
        public UpdateCategoryCommandHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            //track özelliği bulunan bir repo metota ihtiyacımız var o yüzden getbyıd.
            //CONNECTED ENTİTY
            var updatedEntity = await _repository.GetByIdAsync(request.Id);
            if(updatedEntity != null)
            {
                //bu aşamada entity entry state => modified oluyor zaten aşağıda UpdateAsync çalıştığında zaten statei modified olan bir şey tekrar modified olarak işaretleniyor. o nedenle disconnected entityde olduğu gibi repo update metotuna ilişki kurmak amacıyla ihtiyacım olmuyor. Bu nedenle burada aslında repo update metotumu kullanmaya gerek yok. onun yerine repo savechange alıp değişikliğimi yapabilirim.
                //2 tip entity var, connected entity id değeriyle bir entity yakaladığımızda bu connected entity oluyor. Diğeri ise disconnected entity.Burada yaptığımız örnek connected entity oluyor.
                updatedEntity.Definition = request.Definition;
                //burada bunu kullanmama gerek yok zaten connected yöntem ile çalıştığım için yukarıda state modify olarak setlendi repo savechange atıp direkt değişiklikleri kaydedebilirim.
                //await _repository.UpdateAsync(updatedEntity);
                await _repository.CommitAsync();
            }

            //DİSCONNECTED ENTİTY ÖRNEĞİ YAPALIM
            //burda önemli nokta id gönderirsem update ediyor id göndermezsem yeni kayıt oluşturuyor.
                //var updatedCategory = new Category
                //{
                //    Definition = request.Definition,
                //    Id = request.Id,
                //};
            //bu noktoda updatedCategory örneği ile savechanges arasında bir bağlantı yok ef core un bunun modified olarak anlaması için bu noktada devreye repo update giriyor.
            //entity örneğinde ıd alanı doluysa repo update metotu bunu modify olarak setler ama eğer ıd alanı yoksa added olarak setler.
            //ef core un ilk sürümlerinde addorupdate diye bir metot vardır.
                //await _repository.UpdateAsync(updatedCategory);
                //await _repository.CommitAsync();
            return Unit.Value;
        }
    }
}
