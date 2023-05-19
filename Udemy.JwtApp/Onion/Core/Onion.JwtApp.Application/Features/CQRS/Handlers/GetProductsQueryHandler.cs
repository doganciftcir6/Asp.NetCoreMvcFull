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
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQueryRequest, List<ProductListDto>>
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;
        public GetProductsQueryHandler(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductListDto>> Handle(GetProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync();
            return _mapper.Map<List<ProductListDto>>(products);
        }
    }
}
