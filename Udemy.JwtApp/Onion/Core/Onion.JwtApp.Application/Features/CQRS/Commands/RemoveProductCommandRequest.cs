using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.JwtApp.Application.Features.CQRS.Commands
{
    public class RemoveProductCommandRequest : IRequest
    {
        public int Id { get; set; }

        //burada bu id yi api tarafında route tan alacağım için ctor oluşturuyorum.
        //route tan girilen ıd parametre içindeki küçük ıd ye düşer ve bu classın prop Id sine setlenir.
        public RemoveProductCommandRequest(int ıd)
        {
            Id = ıd;
        }
    }
}
