using MediatR;
using Onion.JwtApp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.JwtApp.Application.Features.CQRS.Queries
{
    public class GetCategoryQueryRequest : IRequest<CategoryListDto?>
    {
        public int Id { get; set; }

        public GetCategoryQueryRequest(int ıd)
        {
            Id = ıd;
        }
    }
}
