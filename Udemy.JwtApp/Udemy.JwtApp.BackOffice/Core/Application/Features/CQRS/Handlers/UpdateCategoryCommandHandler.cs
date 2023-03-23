using MediatR;
using Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Commands;
using Udemy.JwtApp.BackOffice.Core.Application.Interfaces;
using Udemy.JwtApp.BackOffice.Core.Domain;

namespace Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Handlers
{
    //<> burada ilk önce requestim ne daha sonra geriye ne dönücem.
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest>
    {
        private readonly IRepository<Category> _repository;

        public UpdateCategoryCommandHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }

        //ilgili productı update işlemimi bu implement metot içinde yapıyorum.
        public async Task<Unit> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            //önce update edeceğimiz kayıdı çekelim.
            var updatedProduct = await _repository.GetByIdAsync(request.Id);
            //ıd ye sahip kayıt db de varsa işlem yapılır.
            if (updatedProduct != null)
            {
                //dbde bulunan kayıdın alanlarına dışarıdan gelen alanların bilgisini atıp güncelleyelim.
                updatedProduct.Definition = request.Definition;

                await _repository.UpdateAsync(updatedProduct);
            }
            return Unit.Value;
        }
    }
}
