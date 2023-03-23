using MediatR;
using Udemy.JwtApp.BackOffice.Core.Application.Dtos;

namespace Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Queries
{
    //Bu query geriye liste olarak CategoryListDto dönecek.
    public class GetAllCategoriesQueryRequest : IRequest<List<CategoryListDto>>
    {
    }
}
