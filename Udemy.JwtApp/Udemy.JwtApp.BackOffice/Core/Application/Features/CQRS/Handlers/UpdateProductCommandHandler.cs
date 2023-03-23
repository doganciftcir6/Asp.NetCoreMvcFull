using MediatR;
using Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Commands;
using Udemy.JwtApp.BackOffice.Core.Application.Interfaces;
using Udemy.JwtApp.BackOffice.Core.Domain;

namespace Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Handlers
{
    //<> burada ilk önce requestim ne daha sonra geriye ne dönücem.
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
    {
        private readonly IRepository<Product> _repository;

        public UpdateProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        //ilgili productı update işlemimi bu implement metot içinde yapıyorum.
        public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            //önce update edeceğimiz kayıdı çekelim.
            var updatedProduct = await _repository.GetByIdAsync(request.Id);
            //ıd ye sahip kayıt db de varsa işlem yapılır.
            if(updatedProduct != null)
            {
                //dbde bulunan kayıdın alanlarına dışarıdan gelen alanların bilgisini atıp güncelleyelim.
                updatedProduct.CategoryId = request.CategoryId;
                updatedProduct.Stock = request.Stock;
                updatedProduct.Price = request.Price;
                updatedProduct.Name = request.Name;

               await _repository.UpdateAsync(updatedProduct);
            }
            return Unit.Value;
        }
    }
}
