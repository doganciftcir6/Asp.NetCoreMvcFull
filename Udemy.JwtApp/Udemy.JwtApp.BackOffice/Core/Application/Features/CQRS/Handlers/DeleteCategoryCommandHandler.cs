using MediatR;
using Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Commands;
using Udemy.JwtApp.BackOffice.Core.Application.Interfaces;
using Udemy.JwtApp.BackOffice.Core.Domain;

namespace Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Handlers
{
    //<> burada ilk önce requestim ne daha sonra geriye ne dönücem.
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest>
    {
        private readonly IRepository<Category> _repository;
        public DeleteCategoryCommandHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }


        //ilgili id'deki productı silme işlemimi bu implement metot içinde yapıyorum.
        public async Task<Unit> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            //silmem gereken kayıdı bulmalıyım.
            var deletedEntity = await _repository.GetByIdAsync(request.Id);
            //eğer null değilse yani ilgili ıd'deki kayıt bulunduysa işlemi gerçekleştir.
            if (deletedEntity != null)
            {
                await _repository.RemoveAsync(deletedEntity);
            }
            //eğer ıd deki kayıt bulunmadıysa geriye boş dön demek gibi bir şey bu.
            return Unit.Value;
        }
    }
}
