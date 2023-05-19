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
            var updatedEntity = await _repository.GetByIdAsync(request.Id);
            if(updatedEntity != null)
            {
                updatedEntity.Definition = request.Definition;
                await _repository.UpdateAsync(updatedEntity);
            }
            return Unit.Value;
        }
    }
}
