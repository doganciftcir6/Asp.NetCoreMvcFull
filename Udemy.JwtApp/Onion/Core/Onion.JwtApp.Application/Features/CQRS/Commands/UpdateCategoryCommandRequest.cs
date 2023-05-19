using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.JwtApp.Application.Features.CQRS.Commands
{
    //savechanges aslında geriye int türünde ne kadar satırda işlem yapıldığını söyler. updatei repoda geriye bool döndürüp update işlemi sonrasında eğer etkilenen kayıt sayısı 0 dan büyükse true küçükse false dönebiliriz. Kullanılan bir yöntem. 
    public class UpdateCategoryCommandRequest : IRequest
    {
        public int Id { get; set; }
        public string? Definition { get; set; }
    }
}
