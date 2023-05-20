using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.JwtApp.Application.Features.CQRS.Commands
{
    //update te bahsettiğimiz geriye dönme olayını burda da uygulamak mümkün ama update te olduğu gibi burda da yapmıcaz şimdilik. Bool değer dönme olayı.
    //remove işleminde direkt olarak bir kaydı silmek mantıklı değildir. Yapılması gereken aslında delete işlemi sırasında bir update yaparak bool olacak olan isdeleted prop alanını kolonunu false şeklinde değiştirmektir. Çünkü veriler çok önemlidir direkt olarak silmek mantıklı değildir. Bu şekilde bir yaklaşım daha faydalıdır.
    public class RemoveCategoryCommandRequest : IRequest
    {
        public int Id { get; set; }
        //burada bu id yi api tarafında route tan alacağım için ctor oluşturuyorum.,
        //route tan girilen ıd parametre içindeki küçük ıd ye düşer ve bu classın prop Id sine setlenir.
        public RemoveCategoryCommandRequest(int ıd)
        {
            Id = ıd;
        }
    }
}
