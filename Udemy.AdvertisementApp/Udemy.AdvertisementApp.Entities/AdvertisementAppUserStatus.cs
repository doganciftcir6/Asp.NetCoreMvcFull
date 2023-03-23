using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy.AdvertisementApp.Entities
{
    //lookup tables
    public class AdvertisementAppUserStatus : BaseEntity
    {
        public string Definition { get; set; }
        //ilişki
        public List<AdvertisementAppUser> AdvertisementAppUsers { get; set; }
    }
}
