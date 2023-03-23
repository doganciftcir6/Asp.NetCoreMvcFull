using MediatR;
using Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Commands;
using Udemy.JwtApp.BackOffice.Core.Application.Interfaces;
using Udemy.JwtApp.BackOffice.Core.Domain;

namespace Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Handlers
{
    //<> burada ilk önce requestim ne daha sonra geriye ne dönücem.
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest>
    {
        private readonly IRepository<Product> _repository;

        public CreateProductCommandHandler(Interfaces.IRepository<Product> repository)
        {
            _repository = repository;
        }

        //ilgili create işlemimi bu implement metot içinde yapıyorum.
        public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            //yeni oluşacak product kayıdının alanlarına dışarıdan gelecek alanları atıyoruz.
            await _repository.CreateAsync(new Product
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
            });

            return Unit.Value;
        }
    }
}
