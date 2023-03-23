using AutoMapper;
using Udemy.JwtApp.BackOffice.Core.Application.Dtos;
using Udemy.JwtApp.BackOffice.Core.Domain;

namespace Udemy.JwtApp.BackOffice.Core.Application.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryListDto>().ReverseMap();
        }
    }
}
