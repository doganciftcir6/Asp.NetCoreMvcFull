using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.JwtApp.Application.Features.CQRS.Commands
{
    //savechanges aslında geriye int türünde ne kadar satırda işlem yapıldığını söyler. updatei repoda geriye bool döndürüp update işlemi sonrasında eğer etkilenen kayıt sayısı 0 dan büyükse true küçükse false dönebiliriz. Kullanılan bir yöntem. 
    public class UpdateProductCommandRequest : IRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
