using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Query.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        //Bir bloğun birden fazla yorumu olabilir ilişkisi 
        //Bir yorumun illa bir bloğu olması gerektiği için
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
