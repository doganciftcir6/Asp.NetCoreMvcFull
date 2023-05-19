using AutoMapper;
using MediatR;
using Onion.JwtApp.Application.Dtos;
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
    //create işlemi sırasında create olan kayıdın idsini geri dönücez.
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, CreatedCategoryDto?>
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;
        public CreateCategoryCommandHandler(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CreatedCategoryDto?> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Definition = request.Definition
            };
            var result = await _repository.CreateAsync(category);
            return _mapper.Map<CreatedCategoryDto>(result);
        }
    }
}
