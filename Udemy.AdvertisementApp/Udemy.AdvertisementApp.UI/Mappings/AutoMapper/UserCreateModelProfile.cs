using AutoMapper;
using Udemy.AdvertisementApp.Dtos;
using Udemy.AdvertisementApp.UI.Models;

namespace Udemy.AdvertisementApp.UI.Mappings.AutoMapper
{
    //MODEL İÇİN MAPPER PROFİLİ
    public class UserCreateModelProfile : Profile
    {
        public UserCreateModelProfile()
        {
            CreateMap<UserCreateModel, AppUserCreateDto>();
        }
    }
}
