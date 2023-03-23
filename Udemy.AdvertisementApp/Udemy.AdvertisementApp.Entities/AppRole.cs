using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy.AdvertisementApp.Entities
{
    public class AppRole : BaseEntity
    {
        public string Definition { get; set; }
        //çoka çok ilişki
        public List<AppUserRole> AppUserRoles { get; set; }
    }
}
