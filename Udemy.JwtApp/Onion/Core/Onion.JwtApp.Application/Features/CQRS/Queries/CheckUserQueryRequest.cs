using MediatR;
using Onion.JwtApp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.JwtApp.Application.Features.CQRS.Queries
{
    public class CheckUserQueryRequest : IRequest<CheckUserResponseDto>
    {
        //null olamaz diyoruz.
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
