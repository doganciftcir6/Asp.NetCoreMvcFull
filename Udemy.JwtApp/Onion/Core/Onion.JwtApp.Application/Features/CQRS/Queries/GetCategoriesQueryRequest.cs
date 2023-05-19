using MediatR;
using Onion.JwtApp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.JwtApp.Application.Features.CQRS.Queries
{
    //MediatR pattern hangi querye karşılık hangi handler ı çalıştıracağımızı interfaceler aracılığıyla belirleyen pattern.
    public class GetCategoriesQueryRequest : IRequest<List<CategoryListDto>>
    {
    }
}
