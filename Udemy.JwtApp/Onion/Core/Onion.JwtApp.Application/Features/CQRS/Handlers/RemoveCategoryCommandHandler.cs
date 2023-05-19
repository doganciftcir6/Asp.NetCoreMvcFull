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
    //remove işleminde direkt olarak bir kaydı silmek mantıklı değildir. Yapılması gereken aslında delete işlemi sırasında bir update yaparak bool olacak olan isdeleted prop alanını kolonunu false şeklinde değiştirmektir. Çünkü veriler çok önemlidir direkt olarak silmek mantıklı değildir. Bu şekilde bir yaklaşım daha faydalıdır.
    public class RemoveCategoryCommandHandler : IRequestHandler<RemoveCategoryCommandRequest>
    {
        private readonly IRepository<Category> _repository;
        public RemoveCategoryCommandHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(RemoveCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            //track özelliği bulunan bir repo metota ihtiyacımız var o yüzden getbyıd.
            var deletedEntity = await _repository.GetByIdAsync(request.Id);
            if(deletedEntity != null)
            {
                await _repository.RemoveAsync(deletedEntity);
            }
            return Unit.Value;
        }
    }
}
