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
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommentRequest, CreatedProductDto?>
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CreatedProductDto?> Handle(CreateProductCommentRequest request, CancellationToken cancellationToken)
        {
            var data = await _repository.CreateAsync(new Product
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
            });
            return _mapper.Map<CreatedProductDto>(data);
        }
    }
}
