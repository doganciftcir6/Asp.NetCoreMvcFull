using MediatR;
using Udemy.JwtApp.BackOffice.Core.Application.Dtos;

namespace Udemy.JwtApp.BackOffice.Core.Application.Features.CQRS.Queries
{
    //Bu query geriye liste olarak ProductListDto dönecek.
    public class GetAllProductsQueryRequest : IRequest<List<ProductListDto>>
    {
    }
}
