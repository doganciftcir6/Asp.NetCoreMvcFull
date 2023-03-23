using AutoMapper;
using MediatR;
using Udemy.JwtApp.BackOffice.Core.Application.Dtos;
using Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Queries;
using Udemy.JwtApp.BackOffice.Core.Application.Interfaces;
using Udemy.JwtApp.BackOffice.Core.Domain;

namespace Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Handlers
{
    //<> burada ilk önce requestim ne daha sonra geriye ne dönücem.
    public class GetCategoryQueryByIdHandler : IRequestHandler<GetCategoryQueryByIdRequest, CategoryListDto>
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public GetCategoryQueryByIdHandler(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //ilgili id'deki productı geriye dönme işlemimi bu implement metot içinde yapıyorum.
        public async Task<CategoryListDto> Handle(GetCategoryQueryByIdRequest request, CancellationToken cancellationToken)
        {
            var dataEntity = await _repository.GetByFilterAsync(x => x.Id == request.Id);
            //GetByFilterAsync metotu geriye bir entity döndürüyor dolayısıyla bu metotun ProductListDto geriye döndürmesiyle uyuşmuyor bu sorunu AutoMapper ile çözelim.
            return _mapper.Map<CategoryListDto>(dataEntity);
        }
    }
}
