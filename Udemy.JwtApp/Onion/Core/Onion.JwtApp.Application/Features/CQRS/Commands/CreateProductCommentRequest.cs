using MediatR;
using Onion.JwtApp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.JwtApp.Application.Features.CQRS.Commands
{
    //kategorideki olayı burda da yapalım geriye create edilen kaydın ıdsini versin. Ama farklı olarak prdocutin namesini de döndürelim.
    public class CreateProductCommentRequest : IRequest<CreatedProductDto?>
    {
        //create işlemi yaparken bana hangi alanlar lazımdır?
        public string? Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
