using AutoMapper;
using MediatR;
using Udemy.JwtApp.BackOffice.Core.Application.Dtos;
using Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Queries;
using Udemy.JwtApp.BackOffice.Core.Application.Interfaces;
using Udemy.JwtApp.BackOffice.Core.Domain;

namespace Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Handlers
{
    //<> burada ilk önce requestim ne daha sonra geriye ne dönücem.
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQueryRequest, List<CategoryListDto>>
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //ilgili categorileri geriye liste olarak dönme işlemimi bu implement metot içinde yapıyorum.
        public async Task<List<CategoryListDto>> Handle(GetAllCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            //bu metot diyor ki ben geriye category listesi dönerim o yüzden categorylistdto'yu bilmiyorum diyor aralarında bir uyuşmazlık oluyor metotun geriye döneceği categoryListDto ile bu noktada işte automapper yardımımıza koşuyor.
            var dataEntity = await _repository.GetAllAsync();
            //burdan gelen datayı entity'i CategoryListDto'ya çevirebil demiş olalaım AutoMapper sayesinde uyuşmazlıktan kurtulalım.
            return _mapper.Map<List<CategoryListDto>>(dataEntity);
        }
    }
}
