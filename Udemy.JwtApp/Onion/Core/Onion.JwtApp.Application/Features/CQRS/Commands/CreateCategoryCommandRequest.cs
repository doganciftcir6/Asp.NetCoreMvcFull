using MediatR;
using Onion.JwtApp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.JwtApp.Application.Features.CQRS.Commands
{
    //create işlemi sırasında genellikle client tarafından create olan kayıdın idsi veya başka bir bilgisi istenir biz id sini geriye döneceğiz. Bu zorunlu değildir geriye bir şeyde döndürmeyebilirsin isteğe göre şekillenen bir durum.
    public class CreateCategoryCommandRequest : IRequest<CreatedCategoryDto?>
    {
        public string? Definition { get; set; }
    }
}
