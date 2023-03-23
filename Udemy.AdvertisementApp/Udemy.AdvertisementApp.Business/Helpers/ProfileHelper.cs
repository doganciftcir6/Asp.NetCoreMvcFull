using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.AdvertisementApp.Business.Mappings.AutoMapper;

namespace Udemy.AdvertisementApp.Business.Helpers
{
    //bu class mapperlar için var
    public static class ProfileHelper
    {
        //Böyle yapmamızın sebebi model için startup'ta model mapper'ı tanımlayabilmek yoksa ordaki ayarlama bunları etkisiz kılıyor eziyor.
        public static List<Profile> GetProfiles()
        {
            //Oluşturmuş olduğum entity ve dtoların Automapper profillerini projeye eklemek için
            return new List<Profile>
                {
                   //profilleri ekleyeceğiz.
                   //opt.AddProfile();
                    new ProvidedServiceProfile(),
                    new AdvertisementProfile(),
                    new AppUserProfile(),
                    new GenderProfile(),
                    new AppRoleProfile(),
                    new AdvertisementAppUserProfile(),
                    new AdvertisementAppUserStatusProfile(),
                    new MilitaryStatusProfile(),
                };
        }
    }
}
