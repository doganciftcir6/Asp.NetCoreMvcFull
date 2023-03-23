using AutoMapper;
using Udemy.JwtApp.BackOffice.Core.Application.Dtos;
using Udemy.JwtApp.BackOffice.Core.Domain;

namespace Udemy.JwtApp.BackOffice.Core.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            //Product'ı ProductListDto'ya çevirebil ve ters işlemide gerçekleştirebil.
            CreateMap<Product, ProductListDto>().ReverseMap();
        }
    }
}
