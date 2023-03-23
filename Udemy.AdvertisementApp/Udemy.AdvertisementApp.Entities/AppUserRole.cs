using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy.AdvertisementApp.Entities
{
    public class AppUserRole : BaseEntity
    {
        public int AppUserId { get; set; }
        //nav prop
        public AppUser AppUser { get; set; }

        public int AppRoleId { get; set; }
        //nav prop
        public AppRole AppRole { get; set; }
    }
}
