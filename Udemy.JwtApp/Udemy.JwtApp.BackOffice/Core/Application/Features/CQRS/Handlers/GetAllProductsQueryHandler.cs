using AutoMapper;
using MediatR;
using Udemy.JwtApp.BackOffice.Core.Application.Dtos;
using Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Queries;
using Udemy.JwtApp.BackOffice.Core.Application.Interfaces;
using Udemy.JwtApp.BackOffice.Core.Domain;

namespace Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Handlers
{
    //<> burada ilk önce requestim ne daha sonra geriye ne dönücem.
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, List<ProductListDto>>
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper mapper;

        public GetAllProductsQueryHandler(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }

        //ilgili productları geriye liste olarak dönme işlemimi bu implement metot içinde yapıyorum.
        public async Task<List<ProductListDto>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            //bu metot diyor ki ben geriye product listesi dönerim o yüzden productlistdto'yu bilmiyorum diyor aralarında bir uyuşmazlık oluyor metotun geriye döneceği productlistDto ile bu noktada işte automapper yardımımıza koşuyor.
            var data = await _repository.GetAllAsync();
            //burdan gelen datayı ProductListDto'ya çevirebil demiş olalaım AutoMapper sayesinde uyuşmazlıktan kurtulalım.
            return this.mapper.Map<List<ProductListDto>>(data);
        }
    }
}
