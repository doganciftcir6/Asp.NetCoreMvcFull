using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.AdvertisementApp.Common;
using Udemy.AdvertisementApp.Dtos;
using Udemy.AdvertisementApp.Entities;

namespace Udemy.AdvertisementApp.Business.Interfaces
{
    public interface IAdvertisementService : IService<AdvertisementCreateDto,AdvertisementUpdateDto,AdvertisementListDto,Advertisement>
    {
        //getall metotu işime yaramıyor tüm kayıtları getiriyor ama ben sadece aktif olan kayıtları çekicem ayrıyetten metota ihtiyacım var
        Task<IResponse<List<AdvertisementListDto>>> GetActivesAsync();
    }
}
