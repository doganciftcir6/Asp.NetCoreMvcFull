using AutoMapper;
using MediatR;
using Onion.JwtApp.Application.Dtos;
using Onion.JwtApp.Application.Features.CQRS.Queries;
using Onion.JwtApp.Application.Interfaces;
using Onion.JwtApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.JwtApp.Application.Features.CQRS.Handlers
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQueryRequest, CategoryListDto?>
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;
        public GetCategoryQueryHandler(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoryListDto?> Handle(GetCategoryQueryRequest request, CancellationToken cancellationToken)
        {
           var data = await _repository.GetByFilterAsync(x => x.Id == request.Id);
            return _mapper.Map<CategoryListDto>(data);
        }
    }
}
