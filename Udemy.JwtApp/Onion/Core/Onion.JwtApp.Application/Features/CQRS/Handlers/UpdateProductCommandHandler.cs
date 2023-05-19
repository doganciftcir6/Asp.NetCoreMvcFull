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
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
    {
        private readonly IRepository<Product> _repository;
        public UpdateProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            //track özelliği bulunan bir repo metota ihtiyacımız var o yüzden getbyıd.
            var updatedEntity = await _repository.GetByIdAsync(request.Id);
            if(updatedEntity != null)
            {
                updatedEntity.Name = request.Name;
                updatedEntity.Price = request.Price;
                updatedEntity.Stock = request.Stock;
                updatedEntity.CategoryId = request.CategoryId;
                await _repository.UpdateAsync(updatedEntity);
            }
            return Unit.Value;
        }
    }
}
