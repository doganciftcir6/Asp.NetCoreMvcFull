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
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequest>
    {
        private readonly IRepository<Product> _repository;
        //dependency injection burada ilgili repository örneği ctor içindeki parametrede bulunan repository değişkenine düşer. Daha sonra bu class içerisinde kullanacak olduğumuz _repository değişkenine bu örnek atılarak kullanılır. YANİ ÖNEMLİ NOKTA OLAN ÖRNEK İLK OLARAK ctordaki parametredeki KÜÇÜK repository değişkenine düşüyor.
        public RemoveProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
        {
            var removedEntity = await _repository.GetByIdAsync(request.Id);
            if (removedEntity != null)
            {
                await _repository.RemoveAsync(removedEntity);
            }
            return Unit.Value;
        }
    }
}
